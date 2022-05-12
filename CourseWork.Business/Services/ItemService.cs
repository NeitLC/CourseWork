using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Business.Enums;
using CourseWork.Business.Interfaces;
using CourseWork.Business.Models;
using CourseWork.Business.Utils;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Extension;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Business.Services
{
    public class ItemService : IItemService
    {
        public IUnitOfWork UnitOfWork { get; }

        private readonly IAccountService _accountService;

        private readonly ICollectionService _collectionService;

        public ItemService(
            IUnitOfWork unitOfWork,
            IAccountService accountService,
            ICollectionService collectionService)
        {
            UnitOfWork = unitOfWork;
            _accountService = accountService;
            _collectionService = collectionService;
        }

        public async Task<EntityPageDto<Item>> GetItems(
            int collectionId,
            ClaimsPrincipal userPrincipal,
            int page= 1,
            ItemSort sortState = ItemSort.Default,
            bool isLiked = false,
            bool isCommented = false)
        {
            var currentUser = await _accountService.GetCurrentUser(userPrincipal);
            var collection = await UnitOfWork.Collections.Get(collectionId);
            Func<Item, bool> predicate = item => item.CollectionId == collection.Id;
            if (currentUser != null && (isLiked || isCommented))
            {
                predicate = item =>
                {
                    var expression = item.CollectionId == collection.Id;
                    if (isLiked)
                    {
                        expression &= item.UsersLiked.Contains(currentUser);
                    }
                    if (isCommented)
                    {
                        expression &= item.Comments.Any(
                            comment => comment.UserId == currentUser.Id);
                    }
                    return expression;
                };
            }
            Func<Item, object> sortPredicate = null;
            if (sortState == ItemSort.Like)
            {
                sortPredicate = item => item.UsersLiked.Count;
            }
            return UnitOfWork.Items.Paginate(
                page: page,
                predicate: predicate,
                sortPredicate: sortPredicate,
                includes: new Expression<Func<Item, object>>[] {
                    item => item.Collection.User,
                    item => item.Tags,
                    item => item.Comments,
                    item => item.UsersLiked
                });
        }

        private static IEnumerable<TagModel> DeserializeTags(string json)
        {
            return JsonSerializer.Deserialize<IEnumerable<TagModel>>(json);
        }

        private async Task AddTags(Item model, IEnumerable<TagModel> tags)
        {
            foreach (var newTag in tags)
            {
                var existingTags = UnitOfWork.Tags.Find(tag => tag.Name == newTag.value).ToList();
                model.Tags.Add(!existingTags.Any() ? new Tag() {Name = newTag.value} : existingTags.First());
            }
            await UnitOfWork.SaveAsync();
        }

        public async Task CreateItem(
            ClaimsPrincipal userPrincipal,
            ItemDto itemDto,
            string userId = "")
        {
            if (itemDto.CollectionId != null)
                await _collectionService.CheckRights(userPrincipal, (int) (itemDto.CollectionId));
            
            var tags = DeserializeTags(itemDto.TagsJson);
            
            await using var transaction = await UnitOfWork.Context.Database.BeginTransactionAsync();
            
            var model = MapperUtil.Map<ItemDto, Item>(itemDto);
            
            UnitOfWork.Items.Add(model);
            
            await UnitOfWork.SaveAsync();
            await AddTags(model, tags);
            await transaction.CommitAsync();
        }
        
        public EntityPageDto<Tag> GetTags(string input)
        {
            return UnitOfWork.Tags.Paginate(predicate: tag => tag.Name.Contains(input));
        }

        public async Task<ItemDto> GetItem(
            int itemId,
            int page = 1,
            ClaimsPrincipal claimsPrincipal=null)
        {
            var item = await UnitOfWork.Items.Get(
                itemId,
                item => item.Collection,
                item => item.UsersLiked,
                item => item.Tags);
            
            var mapperConf = new MapperConfiguration(
                    cfg => cfg.CreateMap<Item, ItemDto>()
                    .ForMember(item => item.Comments, opt => opt.Ignore()));
            
            var itemDto = MapperUtil.Map<Item, ItemDto>(item, mapperConfiguration: mapperConf);
            itemDto.Comments = UnitOfWork.Comments.Paginate(page: page,
                predicate: comment => item.Comments.Contains(comment),
                includes: new Expression<Func<Comment, object>>[] {
                    comment => comment.User
                });
            
            if (claimsPrincipal != null)
            {
                var user = await _accountService.GetCurrentUser(claimsPrincipal);
                itemDto.Liked = item.UsersLiked.Contains(user);
            }
            
            var tags = item.Tags.Select(
                item => new TagModel() { value = item.Name });
            
            itemDto.TagsJson = JsonSerializer.Serialize<IEnumerable<TagModel>>(tags);
            
            return itemDto;
        }

        public async Task EditItem(ClaimsPrincipal claimsPrincipal, ItemDto itemDto)
        {
            if (itemDto.CollectionId != null)
                await _collectionService.CheckRights(claimsPrincipal, (int) (itemDto.CollectionId));
            
            await using var transaction = await UnitOfWork.Context.Database.BeginTransactionAsync();
            
            var model = MapperUtil.Map<ItemDto, Item>(itemDto);
            UnitOfWork.Items.Update(model);
            await UnitOfWork.SaveAsync();
            
            model = await UnitOfWork.Items.Get(model.Id, item => item.Tags);
            
            var tags = DeserializeTags(itemDto.TagsJson);
            
            model.Tags.Clear();
            await AddTags(model, tags);
        }

        public async Task<int> DeleteItem(ClaimsPrincipal claimsPrincipal, int itemId)
        {
            var item = await GetItem(itemId);

            var collectionId = (int) item.CollectionId;
            
            await _collectionService.CheckRights(claimsPrincipal, collectionId);
            await UnitOfWork.Items.Delete(itemId);
            await UnitOfWork.SaveAsync();
            
            return collectionId;
        }

        public async Task<LikeDto> LikeItem(ClaimsPrincipal claimsPrincipal, int itemId)
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal);
            var item = await UnitOfWork.Items.Get(itemId, item => item.UsersLiked);
            
            var likeDto = new LikeDto();

            if (item.UsersLiked.Contains(user))
            {
                item.UsersLiked.Remove(user);
                likeDto.Liked = false;
                item.Likes -= 1;
            }
            else
            {
                item.UsersLiked.Add(user);
                likeDto.Liked = true;
                item.Likes += 1;
            }
            
            likeDto.Count = item.Likes;
            
            await UnitOfWork.SaveAsync();
            
            return likeDto;
        }

        public async Task AddComment(ClaimsPrincipal claimsPrincipal, CommentDto commentDto)
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal);
            var item = await UnitOfWork.Items.Get(commentDto.ItemId);
            
            UnitOfWork.Comments.Add(new Comment() 
            { 
                ItemId = item.Id,
                Text = commentDto.Text,
                UserId = user.Id
            });
            
            await UnitOfWork.SaveAsync();
        }

        public IEnumerable<Item> GetLastCreatedItems()
        {
            return UnitOfWork.Context.Items.IncludeMultiple(
                item => item.UsersLiked,
                item => item.Tags,
                item => item.Collection)
                .AsEnumerable()
                .TakeLast(3)
                .Reverse();
        }

        public IEnumerable<TagDto> GetTagsCloud()
        {
            var tags = UnitOfWork.Tags.Find(tag => tag.Items.Any(), tag => tag.Items);
            return tags.Select(tag => new TagDto() 
                {Name = tag.Name, Count = UnitOfWork.Items.Find(item => item.Tags.Contains(tag)).Count()}).ToList();
        }

        public IEnumerable<Item> GetItemsByTag(string tag)
        {
            return UnitOfWork.Items.Find(
                item => item.Tags.Any(itemTag => itemTag.Name == tag),
                includes: new Expression<Func<Item, object>>[] {
                    item => item.Collection.User,
                    item => item.Collection,
                    item => item.Tags,
                    item => item.Comments,
                    item => item.UsersLiked
                });
        }

        public IEnumerable<Item> GetItemsFullTextSearch(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return UnitOfWork.Context.Items.Where(
                        item => item.Name.Contains(query)
                                || item.Collection.Name.Contains(query)
                                || item.Collection.Topic.Contains(query)
                                || item.Collection.Description.Contains(query)
                                || item.Comments.Any(comment => comment.Text.Contains(query))
                                || item.Tags.Any(tag => tag.Name.Contains(query))
                    ).IncludeMultiple(
                        item => item.Collection.User,
                        item => item.Collection,
                        item => item.Tags,
                        item => item.Comments,
                        item => item.UsersLiked
                    )
                    .ToList();
            }
            return new List<Item>();
        }
    }
}

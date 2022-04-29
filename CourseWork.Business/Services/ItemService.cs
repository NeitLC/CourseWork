using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CourseWork.Business.Dto;
using CourseWork.Business.Enums;
using CourseWork.Business.Interfaces;
using CourseWork.Business.Models;
using CourseWork.Business.Utils;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork.Business.Services
{
    public class ItemService : IItemService
    {
        public IUnitOfWork UnitOfWork { get; }

        private readonly AccountService _accountService;
        private CollectionService _collectionService;

        public ItemService(AccountService accountService, CollectionService collectionService, IUnitOfWork unitOfWork)
        {
            _accountService = accountService;
            _collectionService = collectionService;
            UnitOfWork = unitOfWork;
        }

        public Task<EntityPageDto<Item>> GetItems(
            int collectionId,
            ClaimsPrincipal userPrincipal,
            bool isLiked = false,
            bool isCommented = false,
            ItemSort itemSort = default,
            int pageSize = 10,
            int page = 1)
        {
            throw new System.NotImplementedException();
        }

        public Task<Item> CreateItem(
            ItemDto itemDto,
            ClaimsPrincipal claimsPrincipal,
            string userId = "")
        {
            throw new System.NotImplementedException();
        }

        public EntityPageDto<Tag> GetTags(string input)
        {
            return UnitOfWork.Tags.Paginate(predicate: tag => tag.Name.Contains(input));
        }

        public async Task<ItemDto> GetItem(int id, int page = 1, ClaimsPrincipal claimsPrincipal = null)
        {
            var item = await UnitOfWork.Items.Get(
                id, 
                item => item.Collection,
                item => item.UsersLiked,
                item => item.Tags
                );

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Item, ItemDto>()
                .ForMember(item => item.Comments,
                    opt => opt.Ignore()));

            var itemDto = MapperUtil.Map<Item, ItemDto>(item, mapperConfiguration: mapperConfiguration);

            itemDto.Comments = UnitOfWork.Comments.Paginate(page: page,
                predicate: comment => item.Comments.Contains(comment),
                includes: new Expression<Func<Comment, object>>[]
                {
                    comment => comment.User 
                });

            if (claimsPrincipal != null)
            {
                var user = await _accountService.GetCurrentUser(claimsPrincipal);
                itemDto.Liked = item.UsersLiked.Contains(user);
            }

            var tags = item.Tags.Select(item => new TagModel()
            {
                Value = item.Name
            });
            
            itemDto.TagJson = JsonSerializer.Serialize<IEnumerable<TagModel>>(tags);

            return itemDto;
        }

        public Task EditItem(int id, ItemDto itemDto, ClaimsPrincipal claimsPrincipal)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteItem(int id, ClaimsPrincipal claimsPrincipal)
        {
            throw new System.NotImplementedException();
        }

        public Task<LikeDto> LikeItem(int id, ClaimsPrincipal claimsPrincipal)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Item> GetItemsByTag(string tag)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TagDto> GetTagsCloud()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Item> GetLastItems()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Item> GetItemsByText(string text)
        {
            throw new System.NotImplementedException();
        }

        public Task AddComment(ClaimsPrincipal claimsPrincipal, CommentDto commentDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
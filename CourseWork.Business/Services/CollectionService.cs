using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseWork.Business.Dto;
using CourseWork.Business.Exсeptions;
using CourseWork.Business.Interfaces;
using CourseWork.Business.Utils;
using CourseWork.Domain.Attributes;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Extension;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CourseWork.Business.Services
{
    public class CollectionService : ICollectionService
    {
        public IUnitOfWork UnitOfWork { get; }
        
        private readonly IConfiguration _configuration;
        
        private readonly IAccountService _accountService;

        public CollectionService(IAccountService accountService, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            UnitOfWork = unitOfWork;
            _accountService = accountService;
            _configuration = configuration;
        }

        public IEnumerable<string> GetTopics()
        {
            var topics = new List<string>();
            var props = typeof(Collection).GetProperties();

            foreach (var prop in props)
            {
                var attributes = (TopicAttribute[]) prop.GetCustomAttributes(typeof(TopicAttribute), false);
                foreach (var attribute in attributes)
                {
                    if (attribute != null)
                    {
                        topics.AddRange(attribute.Topics.ToList<string>());
                    }
                }
            }

            return topics;
        }

        public async Task<CollectionDto> GetCollection(int id)
        {
            var collection = await UnitOfWork.Collections.Get(id);
            if (collection == null)
            {
                throw new CollectionNotFoundException();
            }

            var collectionDto = MapperUtil.Map<Collection, CollectionDto>(collection);
            collectionDto.Topics = GetTopics();
            return collectionDto;
        }

        private async Task DeleteImages(Cloudinary cloudinary, Collection collection)
        {
            var images = UnitOfWork.Images.Find(image => image.CollectionId == collection.Id).ToList();
            foreach (var image in images)
            {
                var deletionParams = new DeletionParams(image.PublicId);
                await cloudinary.DestroyAsync(deletionParams);
                await UnitOfWork.Images.Delete(image.Id);
            }

            await UnitOfWork.SaveAsync();
        }
        
        private async Task UploadImages(Collection collection, List<IFormFile> files)
        {
            var account = new Account(
                _configuration["Cloudinary:Name"],
                _configuration["Cloudinary:ApiKey"],
                _configuration["Cloudinary:ApiSecret"]);
            
            var cloudinary = new Cloudinary(account);
            
            await DeleteImages(cloudinary, collection);
            
            foreach (var uploadResult in files.Select(file => cloudinary.Upload(new ImageUploadParams()
                     {
                         File = new FileDescription(file.FileName, file.OpenReadStream()),
                     })))
            {
                UnitOfWork.Images.Add(new Image
                {
                    ImagePath = uploadResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadResult.PublicId,
                    Collection = collection
                });
                await UnitOfWork.SaveAsync();
            }
        }

        public async Task CreateCollection(
            ClaimsPrincipal claimsPrincipal,
            CollectionDto collectionDto,
            string userId = "")
        {
            await using var transaction = await UnitOfWork.Context.Database.BeginTransactionAsync();
            
            collectionDto.User = await _accountService.GetCurrentUser(claimsPrincipal, userId);
            
            var collection = UnitOfWork.Collections
                .Add(MapperUtil.Map<CollectionDto, Collection>(collectionDto));
            
            UnitOfWork.Collections.Add(collection);
            
            await UnitOfWork.SaveAsync();
            await UploadImages(collection, collectionDto.Files);
            await transaction.CommitAsync();
        }

        public async Task<EntityPageDto<Collection>> GetUserCollections(
            ClaimsPrincipal claimsPrincipal,
            int page = 1,
            string userId = "")
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal, userId);
            
            return UnitOfWork.Collections.Paginate(
                    page: page,
                    predicate: collection => collection.User == user,
                    includes: new Expression<Func<Collection, object>>[] {
                        collection => collection.Images,
                        collection => collection.User
                    });
        }

        public async Task EditCollection(CollectionDto collectionDto, ClaimsPrincipal claimsPrincipal)
        {
            var collection = await CheckRights(claimsPrincipal,(int)collectionDto.Id);
            
            await using var transaction = await UnitOfWork.Context.Database.BeginTransactionAsync();
            
            collectionDto.User = collection.User;
            
            MapperUtil.Map<CollectionDto, Collection>(collectionDto, collection);
            
            UnitOfWork.Collections.Update(collection);
            
            await UploadImages(collection, collectionDto.Files);
            await UnitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }

        public async Task DeleteCollection(int id, ClaimsPrincipal claimsPrincipal)
        {
            var collection = await CheckRights(claimsPrincipal, id);
            
            await using var transaction = await UnitOfWork.Context.Database.BeginTransactionAsync();
            
            await UploadImages(collection, new List<IFormFile>());
            
            await UnitOfWork.Collections.Delete(id);
            await UnitOfWork.SaveAsync();
            await transaction.CommitAsync();
        }

        public EntityPageDto<Collection> GetAllCollections(int page = 1)
        {
            return UnitOfWork.Collections.Paginate(page: page, 
                includes: new Expression<Func<Collection, object>>[]
                {
                    collection => collection.Images,
                    collection => collection.User, 
                });
        }

        public IEnumerable<string> GetImages(int id)
        {
            return UnitOfWork.Images.Find(image => image.CollectionId == id)
                .Select(image => image.ImagePath)
                .ToList();
        }

        public IEnumerable<Collection> GetLargestItemsCount()
        {
            return UnitOfWork.Context.Collections
                .IncludeMultiple(collection => collection.Images, collection => collection.User)
                .OrderByDescending(collection => collection.Items.Count())
                .Take(8);
        }

        public async Task<Collection> CheckRights(ClaimsPrincipal claimsPrincipal, int id)
        {
            var collection = await UnitOfWork.Collections.Get(id, collection => collection.User);

            if (!AuthUtil.CheckRights(claimsPrincipal, collection))
            {
                throw new NoRightsException();
            }

            return collection;
        }
    }
}
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Business.Enums;
using CourseWork.Business.Interfaces;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Services
{
    public class ItemService : IItemService
    {
        public IUnitOfWork UnitOfWork { get; }

        private IAccountService _accountService;

        private ICollectionService _collectionService;
        
        public ItemService(
            IUnitOfWork unitOfWork,
            IAccountService accountService,
            ICollectionService collectionService)
        {
            UnitOfWork = unitOfWork;
            _accountService = accountService;
            _collectionService = collectionService;
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

        public EntityPageDto<Tag> GetTags()
        {
            throw new System.NotImplementedException();
        }

        public Task<ItemDto> GetItem(int id)
        {
            throw new System.NotImplementedException();
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
    }
}
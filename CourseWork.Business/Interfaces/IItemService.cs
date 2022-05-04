using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Business.Enums;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Interfaces
{
    public interface IItemService
    {
        IUnitOfWork UnitOfWork { get; }
        
        Task<EntityPageDto<Item>> GetItems(
            int collectionId,
            ClaimsPrincipal userPrincipal,
            int page=1,
            ItemSort sortState = ItemSort.Default,
            bool isLiked = false,
            bool isCommented = false);
        
        Task CreateItem(
            ClaimsPrincipal userPrincipal,
            ItemDto itemDto,
            string userId = "");
        
        EntityPageDto<Tag> GetTags(string input);
        
        Task<ItemDto> GetItem(int itemId, int page = 1, ClaimsPrincipal claimsPrincipal=null);
        
        Task EditItem(ClaimsPrincipal claimsPrincipal, ItemDto itemDto);
        
        Task<int> DeleteItem(ClaimsPrincipal claimsPrincipal, int itemId);
        
        Task<LikeDto> LikeItem(ClaimsPrincipal claimsPrincipal, int itemId);
        
        Task AddComment(ClaimsPrincipal claimsPrincipal, CommentDto commentDto);
        
        IEnumerable<Item> GetLastCreatedItems();
        
        IEnumerable<TagDto> GetTagsCloud();
        
        IEnumerable<Item> GetItemsByTag(string tag);
        
        IEnumerable<Item> GetItemsFullTextSearch(string query);
    }
}
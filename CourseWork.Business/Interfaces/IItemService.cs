using System.Collections;
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
            bool isLiked = false,
            bool isCommented = false,
            ItemSort itemSort = default,
            int pageSize = 10,
            int page = 1
        );

        EntityPageDto<Tag> GetTags();

        Task<ItemDto> GetItem(int id);

        Task EditItem(int id, ItemDto itemDto, ClaimsPrincipal claimsPrincipal);

        Task<int> DeleteItem(int id, ClaimsPrincipal claimsPrincipal);

        Task<LikeDto> LikeItem(int id, ClaimsPrincipal claimsPrincipal);

        IEnumerable<Item> GetItemsByTag(string tag);

        IEnumerable<TagDto> GetTagsCloud();

        IEnumerable<Item> GetLastItems();

        IEnumerable<Item> GetItemsByText(string text);

    }
}
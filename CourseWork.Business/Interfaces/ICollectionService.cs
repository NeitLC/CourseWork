using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Interfaces
{
    public interface ICollectionService
    {
        IEnumerable<string> GetTopics();
        
        Task<CollectionDto> GetCollection(int id);

        Task<Collection> CreateCollection(
            ClaimsPrincipal claimsPrincipal,
            CollectionDto collectionDto,
            string userId = "");

        Task<EntityPageDto<Collection>> GetUserCollection(
            ClaimsPrincipal claimsPrincipal,
            int page = 1,
            string userId = "");


        Task EditCollection(CollectionDto collectionDto, ClaimsPrincipal claimsPrincipal);
        
        Task DeleteCollection(int id, ClaimsPrincipal claimsPrincipal);

        IEnumerable<EntityPageDto<CollectionDto>> GetAllCollections(
            ClaimsPrincipal claimsPrincipal,
            int page = 1,
            string userId = "");

        IEnumerable<string> GetImages(int id);

        IEnumerable<CollectionDto> GetLargestItemsCount();
        
        Task CheckRights(ClaimsPrincipal claimsPrincipal, int id, string userId = "");
    }
}

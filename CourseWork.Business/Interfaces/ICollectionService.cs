using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Interfaces
{
    public interface ICollectionService
    {
        IUnitOfWork UnitOfWork { get; }
        IEnumerable<string> GetTopics();
        
        Task<CollectionDto> GetCollection(int id);

        Task CreateCollection(
            ClaimsPrincipal claimsPrincipal,
            CollectionDto collectionDto,
            string userId = "");

        Task<EntityPageDto<Collection>> GetUserCollections(
            ClaimsPrincipal claimsPrincipal,
            int page = 1,
            string userId = "");


        Task EditCollection(CollectionDto collectionDto, ClaimsPrincipal claimsPrincipal);
        
        Task DeleteCollection(int id, ClaimsPrincipal claimsPrincipal);

        EntityPageDto<Collection> GetAllCollections(int page = 1);
        
        IEnumerable<Collection> GetLargestItemsCount();
        
        Task<Collection> CheckRights(ClaimsPrincipal claimsPrincipal, int id);
        
        IEnumerable<string> GetImages(int id);
    }
}

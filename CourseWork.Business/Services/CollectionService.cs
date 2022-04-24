using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Business.Interfaces;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Services
{
    public class CollectionService : ICollectionService
    {
        public IEnumerable<string> GetTopics()
        {
            throw new System.NotImplementedException();
        }

        public Task<CollectionDto> GetCollection(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Collection> CreateCollection(
            ClaimsPrincipal claimsPrincipal,
            CollectionDto collectionDto,
            string userId = "")
        {
            throw new System.NotImplementedException();
        }

        public Task<EntityPageDto<Collection>> GetUserCollection(
            ClaimsPrincipal claimsPrincipal,
            int page = 1,
            string userId = "")
        {
            throw new System.NotImplementedException();
        }

        public Task EditCollection(CollectionDto collectionDto, ClaimsPrincipal claimsPrincipal)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCollection(int id, ClaimsPrincipal claimsPrincipal)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<EntityPageDto<CollectionDto>> GetAllCollections(
            ClaimsPrincipal claimsPrincipal,
            int page = 1,
            string userId = "")
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetImages(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CollectionDto> GetLargestItemsCount()
        {
            throw new System.NotImplementedException();
        }

        public Task CheckRights(ClaimsPrincipal claimsPrincipal, int id, string userId = "")
        {
            throw new System.NotImplementedException();
        }
    }
}
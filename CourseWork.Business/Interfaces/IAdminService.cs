using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetUsers(ClaimsPrincipal claimsPrincipal);

        Task DeleteUser(string userId);

        Task BlockUser(string userId);

        Task AddAdmin(string userId);
    }
}
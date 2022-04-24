using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Business.Models;
using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Authentication;

namespace CourseWork.Business.Interfaces
{
    public interface IAccountService
    {
        AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl);
        Task<User> GetCurrentUser(ClaimsPrincipal userPrincipal, string userId = "");
        Task<AccountDto> ExternalLogin();
        Task Logout();
    }
}
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Business.Exсeptions;
using CourseWork.Business.Interfaces;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;
using CourseWork.Domain.Utils;
using Microsoft.AspNetCore.Authentication;

namespace CourseWork.Business.Services
{
    public class AccountService: IAccountService
    {
        private IUnitOfWork UnitOfWork { get; set; }
        
        public AccountService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl)
        {
            return UnitOfWork.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }
        
        public async Task<AccountDto> ExternalLogin()
        {
            var info = await UnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                throw new UserNotLoggedExсeption();
            }
            
            var result = await UnitOfWork.SignInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            
            var userInfo = new AccountDto()
            {
                Name = info.Principal.FindFirst(ClaimTypes.Name)?.Value,
                Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value
            };
            
            if (!result.Succeeded)
            {
                var user = new User
                {
                    UserName = info.Principal.FindFirst(ClaimTypes.Name)?.Value,
                    Email = info.Principal.FindFirst(ClaimTypes.Email)?.Value
                };
                var identityResult = await UnitOfWork.UserManager.CreateAsync(user);
                if (identityResult.Succeeded)
                {
                    identityResult = await UnitOfWork.UserManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded)
                    {
                        await UnitOfWork.SignInManager.SignInAsync(user, false);
                        return userInfo;
                    }
                }
                throw new AccessDeniedException();
            }
            
            return userInfo;
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal userPrincipal, string userId = "")
        {
            if (!string.IsNullOrEmpty(userId) && RoleUtil.IsAdmin(userPrincipal))
            {
                return await UnitOfWork.UserManager.FindByIdAsync(userId);
            }

            return await UnitOfWork.UserManager.GetUserAsync(userPrincipal);
        }
        
        public async Task Logout()
        {
            await UnitOfWork.SignInManager.SignOutAsync();
        }
        
        public async Task Login(string username, string password)
        {
            await UnitOfWork.SignInManager.PasswordSignInAsync(username, password, false, false);
        }

        public async Task Register(string username, string email, string password)
        {
            var user = new User
            {
                UserName = username, Email = email
            };
            
            var result = await UnitOfWork.UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await UnitOfWork.SignInManager.SignInAsync(user, false);
            }
        }
    }
}
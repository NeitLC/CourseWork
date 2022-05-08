using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using CourseWork.Business.Interfaces;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetUsers(ClaimsPrincipal claimsPrincipal)
        {
            var allUsers = await _unitOfWork.Users.GetAll();
            var managedUsers = new List<User>();
            
            foreach (var user in allUsers)
            {
                var roleGroup = await _unitOfWork.UserManager.GetRolesAsync(user);
                if (roleGroup.Count == 0)
                {
                    managedUsers.Add(user);
                }
            }

            return managedUsers;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _unitOfWork.Users.Get(userId,
                user => user.Collections,
                user => user.LikedItems,
                user => user.Comments);
            
            await using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            
            await _unitOfWork.UserManager.DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            await transaction.CommitAsync();

        }

        public async Task BlockUser(string userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user.LockoutEnabled)
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = DateTimeOffset.UtcNow;
            }
            else
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(2);
            }
            
            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.UserManager.UpdateSecurityStampAsync(user);
        }

        public async Task AddAdmin(string userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            await _unitOfWork.UserManager.AddToRoleAsync(user, Role.Admin.ToString().ToLower());
        }
    }
}
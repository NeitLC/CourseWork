using System.Security.Claims;
using CourseWork.Domain.Models;
using CourseWork.Domain.Utils;

namespace CourseWork.Business.Utils
{
    public static class AuthUtil
    {
        public static bool CheckRights(ClaimsPrincipal claimsPrincipal, Collection collection)
        {
            return claimsPrincipal.Identity is {IsAuthenticated: true} 
                   && (claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) 
                       == collection.User.Id || RoleUtil.IsAdmin(claimsPrincipal));
        } 
    }
}
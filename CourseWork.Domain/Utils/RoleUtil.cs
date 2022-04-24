using System.Security.Claims;
using CourseWork.Domain.Enums;

namespace CourseWork.Domain.Utils
{
    public class RoleUtil
    {
        public static string AdminRoleName()
        {
            return Role.Admin.ToString().ToLower();
        }

        public static bool IsAdmin(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(AdminRoleName());
        }
    }
}
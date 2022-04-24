using System.Threading.Tasks;
using CourseWork.Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Domain.Initializers
{
    public class AdminInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleName = RoleUtil.AdminRoleName();
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
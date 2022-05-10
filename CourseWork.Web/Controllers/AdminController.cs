using System.Threading.Tasks;
using CourseWork.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("/UserAdmin")]
        public async Task<IActionResult> Index(int page = 1)
        {
            Response.Cookies.Append("adminPage", page.ToString());
            return View(await _adminService.GetUsers(User));
        }

        public async Task<IActionResult> AddAdmin(string userId)
        {
            await _adminService.AddAdmin(userId);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Block(string userId)
        {
            await _adminService.BlockUser(userId);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(string userId)
        {
            await _adminService.DeleteUser(userId);
            return RedirectToAction("Index");
        }
        
        
    }
}
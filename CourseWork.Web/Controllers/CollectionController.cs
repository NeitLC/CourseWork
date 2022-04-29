using System.Threading.Tasks;
using Collections.ViewModels;
using CourseWork.Business.Dto;
using CourseWork.Business.Interfaces;
using CourseWork.Business.Utils;
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }
        
        [Route("/Collections", Name = "Profile")]
        public async Task<IActionResult> Index(
            [FromQuery] int page = 1,
            [FromQuery] string userId = "")
        {
            var collections = await _collectionService.GetUserCollections(User, page, userId);
            
            var markdown = new Markdown();
            
            foreach (var entity in collections.Entities)
            {
                entity.Description = markdown.Transform(entity.Description);
            }

            ViewData["userId"] = userId;
            Response.Cookies.Append("collectionPage", page.ToString());
            return View(collections);
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string userId = "")
        {
            Response.Cookies.Delete("collectionId");
            ViewData["userId"] = userId;
            return View("Form", new CollectionViewModel
            {
                Topics = _collectionService.GetTopics()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionViewModel collectionViewModel, [FromQuery] string userId = "")
        {
            await _collectionService.CreateCollection(
                User,
                MapperUtil.Map<CollectionViewModel, CollectionDto>(collectionViewModel),
                userId
                );
            return RedirectToAction("Index", new { userId = userId});
        }
    }
}
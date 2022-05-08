using System.Threading.Tasks;
using Collections.ViewModels;
using CourseWork.Business.Dto;
using CourseWork.Business.Enums;
using CourseWork.Business.Interfaces;
using CourseWork.Business.Utils;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Models;
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICollectionService _collectionService;

        public ItemController(
            IItemService itemService,
            ICollectionService collectionService)
        {
            _itemService = itemService;
            _collectionService = collectionService;
        }

        [AllowAnonymous]
        [Route("/Collections/{collectionId}/Items/", Name = "Items")]
        public async Task<IActionResult> Index(
            [FromRoute(Name = "collectionId")] int collectionId,
            [FromQuery] int page = 1,
            [FromQuery] string userId = "",
            [FromQuery] ItemSort sortOrder = ItemSort.Default,
            [FromQuery] bool isLiked = false,
            [FromQuery] bool isCommented = false,
            [FromQuery] string backController = "",
            [FromQuery] string backAction = "")
        {
            Response.Cookies.Append("itemPage", page.ToString());
            
            ViewData["collectionId"] = collectionId.ToString();
            
            Response.Cookies.Append("collectionId", collectionId.ToString());
            Response.Cookies.Append("sortOrder", sortOrder.ToString());
            Response.Cookies.Append("isLiked", isLiked.ToString());
            Response.Cookies.Append("isCommented", isCommented.ToString());
            
            ViewData["userId"] = userId;
            ViewData["controller"] = backController;
            ViewData["action"] = backAction;
            
            var sd = await _itemService
                .GetItems(collectionId, User, page, sortOrder, isLiked, isCommented);
            
            return View(sd);
        }

        public async Task<IActionResult> Create(
            [FromQuery] int collectionId,
            [FromQuery] string userId = "")
        {
            var collectionDto = await _collectionService.GetCollection(collectionId);
            
            var item = new ItemViewModel
            {
                Collection = MapperUtil.Map<CollectionDto, Collection>(collectionDto)
            };
            
            ViewData["userId"] = userId;
            
            return View("Form", item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel model, [FromQuery] string userId = "")
        {
            var itemDto = MapperUtil.Map<ItemViewModel, ItemDto>(model);

            await _itemService.CreateItem(User, itemDto, userId);

            return RedirectToAction(
                "Index",
                new {collectionId = model.CollectionId, page = 1, userId = userId});
        }

        public async Task<IActionResult> Edit(int itemId, [FromQuery] string userId = "")
        {
            var itemDto = await _itemService.GetItem(itemId);
            ViewData["userId"] = userId;
            var collectionDto = await _collectionService.GetCollection((int)itemDto.CollectionId);
            itemDto.Collection = MapperUtil.Map<CollectionDto, Collection>(collectionDto);
            return View("Form", MapperUtil.Map<ItemDto, ItemViewModel>(itemDto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemViewModel model, [FromQuery] string userId = "")
        {
            await _itemService.EditItem(
                User,
                MapperUtil.Map<ItemViewModel, ItemDto>(model));
            return RedirectToAction(
                "Index",
                new { collectionId = model.CollectionId, page = 1, userId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int itemId, [FromQuery] string userId = "")
        {
            var collectionId = await _itemService.DeleteItem(User, itemId);
            return RedirectToAction(
                "Index",
                new { collectionId = collectionId, page = 1, userId = userId });
        }

        [AllowAnonymous]
        [Route("/Items/{itemId}")]
        public async Task<IActionResult> GetItem(
            [FromRoute(Name = "itemId")] int itemId,
            [FromQuery] string userId = "",
            [FromQuery] int page = 1)
        {
            var itemDto = await _itemService.GetItem(itemId, page, User);
            
            var collectionDto = await _collectionService.GetCollection((int)itemDto.CollectionId);
            
            itemDto.Collection = MapperUtil.Map<CollectionDto, Collection>(collectionDto);
            var model = MapperUtil.Map<ItemDto, ItemViewModel>(itemDto);
            
            var markdown = new Markdown();
            
            model.FirstText = markdown.Transform(model.FirstText ?? "");
            model.SecondText = markdown.Transform(model.SecondText ?? "");
            model.ThirdText = markdown.Transform(model.ThirdText ?? "");
            
            if (User.Identity is {IsAuthenticated: true, Name: { }}) 
                Response.Cookies.Append("username", User.Identity.Name);
            
            Response.Cookies.Append("itemId", itemId.ToString());
            
            ViewData["Action"] = "GetItem";
            ViewData["userId"] = userId;
            
            return View("Item", model);
        }
        
        [HttpPost]
        [Route("/Like")]
        public async Task<LikeViewModel> LikeItem(int itemId)
        {
            var likeDto = await _itemService.LikeItem(User, itemId);
            return MapperUtil.Map<LikeDto, LikeViewModel>(likeDto);
        }
        
        [Route("/Tags")]
        public EntityPageDto<Tag> GetTags(string input)
        {
            return _itemService.GetTags(input);
        }
        
        [AllowAnonymous]
        [Route("/Items")]
        public IActionResult GetItemsByTag([FromQuery] string tag)
        {
            return View("Search", _itemService.GetItemsByTag(tag));
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("/FullTextSearch")]
        public IActionResult GetItemsFullTextSearch([FromQuery] string query)
        {
            return View("Search", _itemService.GetItemsFullTextSearch(query));
        }
    }
}

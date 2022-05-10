using System;
using System.Collections.Generic;
using CourseWork.Business.Dto;
using CourseWork.Business.Interfaces;
using CourseWork.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CourseWork.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICollectionService _collectionService;

        public HomeController(ILogger<HomeController> logger, IItemService itemService, ICollectionService collectionService)
        {
            _itemService = itemService;
            _collectionService = collectionService;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel
            {
                LastCreatedItems = _itemService.GetLastCreatedItems(),
                LargestNumberItems = _collectionService.GetLargestItemsCount()
            });
        }

        public IActionResult GetAllCollections(int page = 1)
        {
            Response.Cookies.Append("collectionPage", page.ToString());
            ViewData["action"] = "GetAllCollections";
            ViewData["controller"] = "Home";
            return View("../Collection/Index", _collectionService.GetAllCollections(page));
        }
        
        public IEnumerable<TagDto> GetTagsCloud()
        {
            return _itemService.GetTagsCloud();
        }
        
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)});
            return LocalRedirect(returnUrl);
        }
    }
}
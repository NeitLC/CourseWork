using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collections.ViewModels;
using CourseWork.Business.Dto;
using CourseWork.Business.Interfaces;
using CourseWork.Business.Utils;
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("/Collection/Edit")]
        public async Task<IActionResult> Edit([FromQuery(Name = "collectionId")] int collectionId,
            [FromQuery] string userId = "")
        {
            try
            {
                var collectionDto = await _collectionService.GetCollection(collectionId);
                Response.Cookies.Append("collectionId",collectionId.ToString());
                var collection = MapperUtil.Map<CollectionDto, CollectionViewModel>(collectionDto);
                await _collectionService.CheckRights(User, collectionId);
                ViewData["userId"] = userId;
                return View("Form", collection);
            }
            catch
            {
                return NotFound();
            }   
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(
            CollectionViewModel collectionViewModel, 
            [FromQuery(Name = "collectionId")] int collectionId, 
            [FromQuery] string userId = "")
        {
            try
            {
                collectionViewModel.Id = collectionId;
                await _collectionService.EditCollection(
                    MapperUtil.Map<CollectionViewModel, CollectionDto>(collectionViewModel),
                    User
                );
                return RedirectToAction("Index", new { userId = userId });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromQuery] int collectionId, [FromQuery] string userId = "")
        {
            try
            {
                await _collectionService.DeleteCollection(collectionId, User);
                return RedirectToAction("Index", new { userId = userId });
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("/Collections/{collectionId}/Images")]
        public IEnumerable<string> GetImages(int collectionId)
        {
            return _collectionService.GetImages(collectionId);
        }
    }
}
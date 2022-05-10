using System.Threading.Tasks;
using CourseWork.Business.Dto;
using CourseWork.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CourseWork.Web.Hubs
{
    [Authorize]
    public class CommentHub : Hub
    {
        private readonly IItemService _itemService;

        public CommentHub(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task JoinRoom(string itemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "item-"+itemId);
        }

        public async Task Send(string itemId, string comment)
        {
            await _itemService.AddComment(
                Context.User,
                new CommentDto()
                {
                    ItemId = int.Parse(itemId),
                    Text = comment
                });
            if (Context.User?.Identity != null)
                await Clients.GroupExcept("item-" + itemId, Context.ConnectionId)
                    .SendAsync("Receive", Context.User.Identity.Name, comment);
        }
    }
}
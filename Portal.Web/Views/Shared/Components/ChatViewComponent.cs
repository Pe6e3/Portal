using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components
{
    [ViewComponent(Name = "_Chat")]
    public class ChatViewComponent : ViewComponent
    {
        private readonly UnitOfWork uow;

        public ChatViewComponent(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IViewComponentResult> InvokeAsync(int chatId)
        {
            List<Message> messages = await uow.MessageRep.ListChatMessages(chatId);
            User user = await uow.UserRep.GetUserByLogin(User.Identity.Name);
            ViewBag.Avatar = user.Profile.AvatarImg;
            return View(messages);
        }
    }
}

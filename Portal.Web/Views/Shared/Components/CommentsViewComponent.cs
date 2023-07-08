using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components
{
    [ViewComponent(Name = "_Comments")]
    public class CommentsViewComponent : ViewComponent
    {
        private readonly UnitOfWork uow;

        public CommentsViewComponent(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IViewComponentResult> InvokeAsync(string postSlug, int postId)
        {
            List<Comment>? comments = await uow.CommentRep.GetCommentsByPostId(postId);

            ViewBag.PostSlug = postSlug;
            return View(comments);
        }
    }
}

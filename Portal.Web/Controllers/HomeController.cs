using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Areas.Admin.Controllers;
using Portal.Web.Models;
using Portal.Web.ViewModels;
using System.Diagnostics;

namespace Portal.Web.Controllers
{
    public class HomeController : BaseController<Post, IPostRepository>
    {
        protected new readonly ILogger<BaseController<Post, IPostRepository>> logger;
        private readonly UnitOfWork uow;

        public HomeController(UnitOfWork uow, ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository)
            : base(uow, logger, repository)
        {
            this.logger = logger;
            this.uow = uow;
        }

        public override async Task<IActionResult> Index()
        {
            List<Post> allPosts = (List<Post>)await uow.PostRep.ListAllAsync();
            List<PostContent> allContent = (List<PostContent>)await uow.PostContentRep.ListAllAsync();
            List<PostViewModel> posts = new List<PostViewModel>();

            foreach (Post post in allPosts)
            {
                PostContent? content = allContent.FirstOrDefault(x => x.PostId == post.Id);
                posts.Add(new PostViewModel()
                {
                    Slug = post.Slug,
                    CreatedAt = post.CreatedAt,
                    Id = post.Id,
                    Title = content.Title,
                    PostBody = content.PostBody,
                    PostImage = content.PostImage,
                    PostVideo = content.PostVideo,
                    CommentsClosed = content.CommentsClosed
                });
            }
            return View(posts);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


    }
}
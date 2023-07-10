using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using Portal.Web.ViewModels;
using System.Diagnostics;

namespace Portal.Web.Controllers
{
    public class HomeController : BaseController<Post, IPostRepository>
    {
        protected new readonly ILogger<BaseController<Post, IPostRepository>> logger;
        private readonly UnitOfWork uow;
        private readonly IMapper mapper;

        public HomeController(UnitOfWork uow, ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository, IMapper mapper)
            : base(uow, logger, repository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.uow = uow;
        }

        public override async Task<IActionResult> Index()
        {
            List<Post> allPosts = (List<Post>)await uow.PostRep.ListAll();
            List<PostContent> allContent = (List<PostContent>)await uow.PostContentRep.ListAll();
            List<PostViewModel> posts = new List<PostViewModel>();

            mapper.Map(allPosts, posts);
            mapper.Map(allContent, posts);

            return View(posts);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        public async Task<IActionResult> AccessDenied()
        {
            string? login = User.Identity.Name;
            User? user = await uow.UserRep.GetUserByLogin(login);
            return View(user);
        }
    }
}
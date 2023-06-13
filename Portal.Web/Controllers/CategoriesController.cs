using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Areas.Admin.Controllers;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Controllers
{
    public class CategoriesController : BaseController<Category, ICategoryRepository>
    {
        private readonly UnitOfWork _uow;

        public CategoriesController(UnitOfWork uow, ILogger<BaseController<Category, ICategoryRepository>> logger, ICategoryRepository repository) : base(uow, logger, repository)
        {
            _uow = uow;

        }

        [HttpGet("/category/{*categorySlug}")]
        public async Task<IActionResult> CategoryListIndex(string categorySlug)
        {
            List<PostCategory> postCats = await _uow.CategoryRep.GetPostsByCatSlugAsync(categorySlug);

            ViewBag.CatSlug = categorySlug;
            return View("Index", postCats);
        }


        [HttpGet("/posts/{*postSlug}")]
        public async Task<IActionResult> PostListIndex(string postSlug)
        {
            int postId = await _uow.PostRep.GetPostIdBySlugAsync(postSlug);
            return RedirectToAction("Details", "Home", new { id = postId });
        }


    }

}

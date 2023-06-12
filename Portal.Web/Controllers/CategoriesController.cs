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
            if (postCats == null || postCats.Count == 0)
            {
                var urlHelper = new UrlHelper(ControllerContext);
                var redirectUrl = urlHelper.Action("Index", new { categorySlug = categorySlug });
                return Redirect(redirectUrl);
            }

            ViewBag.CatSlug = categorySlug;
            return View("Index", postCats);
        }

 





    }

}

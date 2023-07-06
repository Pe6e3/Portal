using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components
{
    [ViewComponent(Name = "_CategoryReview")]
    public class CategoryReviewViewComponent : ViewComponent
    {
        private readonly UnitOfWork uow;

        public CategoryReviewViewComponent(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categorySlug, string sectionClass)
        {
            ViewBag.Section = sectionClass;

            List<PostCategory> postCategory = await 
                uow.PostCategoryRep
                   .GetCategoryPosts(categorySlug: categorySlug, count: 4);

            return View(postCategory);
        }
    }
}

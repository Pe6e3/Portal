using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components;

[ViewComponent(Name = "_ThreeCategoryPosts")]
public class ThreeCategoryPostsViewComponent : ViewComponent
{
    private readonly UnitOfWork uow;
    public ThreeCategoryPostsViewComponent(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<IViewComponentResult> InvokeAsync(string categorySlug) =>
        View(await uow.PostCategoryRep.GetCategoryPosts(categorySlug: categorySlug, count: 3));

}

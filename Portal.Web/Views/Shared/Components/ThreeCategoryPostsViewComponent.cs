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

    public async Task<IViewComponentResult> InvokeAsync(int categoryId) =>
        View(await uow.PostCategoryRep.GetCategoryPosts(catId: categoryId, count: 3));

}

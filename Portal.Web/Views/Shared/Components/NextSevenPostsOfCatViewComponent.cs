using Microsoft.AspNetCore.Mvc;
using Portal.BLL;

namespace Portal.Web.Views.Shared.Components;

[ViewComponent(Name = "_NextSevenPostsOfCat")]
public class NextSevenPostsOfCatViewComponent : ViewComponent
{
    private readonly UnitOfWork uow;
    public NextSevenPostsOfCatViewComponent(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<IViewComponentResult> InvokeAsync(string categorySlug) => View(await
            uow.PostCategoryRep
            .GetCategoryPosts(
                categorySlug: categorySlug,
                count: 7,
                skip: 3
                ));

}

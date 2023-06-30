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

    public async Task<IViewComponentResult> InvokeAsync(int categoryId) => View(await
            uow.PostCategoryRep
            .GetCategoryPosts(
                catId: categoryId,
                count: 7,
                skip: 3
                ));

}

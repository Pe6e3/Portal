using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components;

[ViewComponent(Name = "_FiveLastPosts")]
public class FiveLastPostsViewComponent : ViewComponent
{
    private readonly UnitOfWork uow;
    public FiveLastPostsViewComponent(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<IViewComponentResult> InvokeAsync() =>
    View(await uow.PostRep.ListPostsWithComments(count: 5));



}

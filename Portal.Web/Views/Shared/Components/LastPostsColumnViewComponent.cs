using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components;

[ViewComponent(Name = "_LastPostsColumn")]
public class LastPostsColumnViewComponent : ViewComponent
{
    private readonly UnitOfWork uow;
    public LastPostsColumnViewComponent(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<IViewComponentResult> InvokeAsync() => 
        View(await uow.PostRep.ListAllAsync(4, "Content","Comments"));

}

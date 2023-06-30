using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components;

[ViewComponent(Name = "_FiveLastPostsColumn")]
public class FiveLastPostsColumnViewComponent : ViewComponent
{
    private readonly UnitOfWork uow;
    public FiveLastPostsColumnViewComponent(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<IViewComponentResult> InvokeAsync() => 
        View(await uow.PostRep.ListAllAsync(5, "Content","Comments"));

}

using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components;

[ViewComponent(Name = "_MyLogger")]
public class MyLoggerViewComponent : ViewComponent
{
    private readonly UnitOfWork uow;

    public MyLoggerViewComponent(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await uow.UserRep.GetUserByLogin(User.Identity.Name);

        var logger = new MyLogger();
        logger.UserIP = HttpContext.Connection.RemoteIpAddress?.ToString();
        logger.UserClick = HttpContext.Request.Path;
        logger.UserId = user.Id;
        logger.Date = DateTime.UtcNow;

        await uow.MyLoggerRep.InsertAsync(logger);

        var allLogs = await uow.MyLoggerRep.ListAllAsync();
        return View(allLogs);
    }
}

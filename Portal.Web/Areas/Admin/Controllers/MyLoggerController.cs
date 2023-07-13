using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Controllers;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "1")]
public class MyLoggerController : BaseController<Role, IRoleRepository>
{
    private readonly UnitOfWork uow;

    public MyLoggerController(UnitOfWork uow, ILogger<BaseController<Role, IRoleRepository>> logger, IRoleRepository repository) : base(uow, logger, repository)
    {
        this.uow = uow;
    }

    public async Task<IActionResult> ShowUserLog()
    {
        var allLogs = await uow.MyLoggerRep.ListLogs();
        return View("LoggerPage", allLogs);
    }

    public async Task<IActionResult> ShowUserLogWithoutMe()
    {
        var allLogs = await uow.MyLoggerRep.ListLogsWithoutMe();
        return View("LoggerPage", allLogs);
    }
}

using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MenusController : BaseController<Menu, IMenuRepository>
{
    public MenusController(UnitOfWork unitOfWork, ILogger<BaseController<Menu, IMenuRepository>> logger, IMenuRepository repository) : base(unitOfWork, logger, repository)
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MenuItemsController : BaseController<MenuItem, IMenuItemRepository>
{
    public MenuItemsController(UnitOfWork unitOfWork, ILogger<BaseController<MenuItem, IMenuItemRepository>> logger, IMenuItemRepository repository) : base(unitOfWork, logger, repository)
    {
    }
}

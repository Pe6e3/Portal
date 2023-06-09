using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MenuItemsController : BaseController<MenuItem, IMenuItemRepository>
{
    private readonly UnitOfWork uow;
    public MenuItemsController(UnitOfWork uow, ILogger<BaseController<MenuItem, IMenuItemRepository>> logger, IMenuItemRepository repository) : base(uow, logger, repository)
    {
        this.uow = uow;
    }

    public async Task<IActionResult> IndexMenuItem(int id)
    {
        ViewBag.MenuID = id;
        return View("Index", await uow.MenuItemRep.GetByMenuIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> CreateItemInMenuId(int id)
    {
        ViewBag.MenuID = id;
        return View("Create");
    }

}

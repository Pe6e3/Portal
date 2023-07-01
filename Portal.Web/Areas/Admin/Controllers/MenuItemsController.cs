using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.ViewModels;

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
    public override async Task<IActionResult> Delete(int id, int menuId)
    {
        MenuItem mItem = await uow.MenuItemRep.GetByIdAsync(id);
        if (mItem != null)
            await uow.MenuItemRep.DeleteAsync(mItem);
        return RedirectToAction(nameof(IndexMenuItem), new { id = menuId });
    }

    [HttpGet]
    public async Task<IActionResult> CreateItemInMenuId(int id)
    {
        MenuCatPostViewModel mcpwm = new MenuCatPostViewModel();
        mcpwm.Posts = await uow.PostRep.ListAllPostsWithContentsAsync();
        mcpwm.Categories = await uow.CategoryRep.ListAllAsync();
        mcpwm.MenuId = id;

        return View("Create", mcpwm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHttpPost(MenuCatPostViewModel mcpwv)
    {
        if (ModelState.IsValid)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.MenuId = mcpwv.MenuId;
            menuItem.Name = mcpwv.Name;
            menuItem.Position = mcpwv.Position;
            menuItem.Slug = mcpwv.Url;

            await uow.MenuItemRep.InsertAsync(menuItem);
            return RedirectToAction(nameof(IndexMenuItem), new { id = menuItem.MenuId });
        }
        return View("Create", mcpwv);
    }

    [HttpGet]
    public override async Task<IActionResult> Edit(int Id) /*menuItemId*/
    {
        MenuItem menuItem = await uow.MenuItemRep.GetByIdAsync(Id);
        if (menuItem == null)
            return NotFound();
        ViewBag.MenuID = menuItem.MenuId;


        MenuCatPostViewModel mcpwm = new MenuCatPostViewModel();
        mcpwm.Posts = await uow.PostRep.ListAllPostsWithContentsAsync();
        mcpwm.Categories = await uow.CategoryRep.ListAllAsync();
        mcpwm.MenuId = menuItem.MenuId;
        mcpwm.Position = menuItem.Position;
        mcpwm.Name = menuItem.Name;
        mcpwm.Url = menuItem.Slug;
        mcpwm.Id = menuItem.Id;

        return View(mcpwm);
    }

    [HttpPost]
    public async Task<IActionResult> EditPost(MenuCatPostViewModel mcpwm)
    {
        //ViewBag.MenuID = menuItem.MenuId;
        if (ModelState.IsValid)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.MenuId = mcpwm.MenuId;
            menuItem.Name = mcpwm.Name;
            menuItem.Position = mcpwm.Position;
            menuItem.Slug = mcpwm.Url;
            menuItem.Id = mcpwm.Id;

            await uow.MenuItemRep.UpdateAsync(menuItem);
            return RedirectToAction(nameof(IndexMenuItem), new { id = menuItem.MenuId });
        }

        return View(mcpwm);
    }

}

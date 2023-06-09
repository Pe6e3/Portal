﻿using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MenusController : BaseController<Menu, IMenuRepository>
{
    private readonly UnitOfWork uow;
    public MenusController(UnitOfWork uow, ILogger<BaseController<Menu, IMenuRepository>> logger, IMenuRepository repository) : base(uow, logger, repository)
    {
        this.uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        @ViewBag.MenuId = 22;
        return View();
    }



}

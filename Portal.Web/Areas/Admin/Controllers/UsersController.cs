using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : BaseController<User, IUserRepository>
{
    private readonly UnitOfWork uow;

    public UsersController(UnitOfWork uow, ILogger<BaseController<User, IUserRepository>> logger, IUserRepository repository) : base(uow, logger, repository)
    {
        this.uow = uow;
    }


    public override async Task<IActionResult> Index()
    {

        return View(await uow.UserRep.GetAllUsers());
    }
}

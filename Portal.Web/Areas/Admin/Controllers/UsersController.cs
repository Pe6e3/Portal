using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : BaseController<User, UserRepository>
{
    public UsersController(ILogger<BaseController<User, UserRepository>> logger, UserRepository repository)
        : base(logger, repository)
    {
    }
}
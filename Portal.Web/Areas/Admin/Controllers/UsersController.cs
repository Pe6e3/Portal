using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : BaseController<User, IUserRepository>
{
    public UsersController(ILogger<BaseController<User, IUserRepository>> logger, IUserRepository repository) : base(logger, repository)
    {
    }
}

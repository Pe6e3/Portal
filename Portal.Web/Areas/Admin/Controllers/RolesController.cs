using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class RolesController : BaseController<Role, IRoleRepository>
{
    public RolesController(ILogger<BaseController<Role, IRoleRepository>> logger, IRoleRepository repository) : base(logger, repository)
    {
    }
}

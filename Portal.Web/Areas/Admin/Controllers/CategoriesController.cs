using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : BaseController<Category, ICategoryRepository>
{
    public CategoriesController(UnitOfWork unitOfWork, ILogger<BaseController<Category, ICategoryRepository>> logger, ICategoryRepository repository): base(unitOfWork, logger, repository)
    {
    }
}

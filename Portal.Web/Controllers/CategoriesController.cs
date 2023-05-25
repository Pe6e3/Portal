﻿using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Areas.Admin.Controllers;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Controllers
{
    public class CategoriesController : BaseController<Category, ICategoryRepository>
    {
        public CategoriesController(ILogger<BaseController<Category, ICategoryRepository>> logger, ICategoryRepository repository) : base(logger, repository)
        {
        }
    }

}

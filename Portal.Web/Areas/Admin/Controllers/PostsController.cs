using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portal.DAL.Entities;
using Portal.BLL;
using Portal.DAL.Interfaces;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PostsController : BaseController<Post, IPostRepository>
{
    public PostsController(ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository)
        : base(logger, repository)
    {
    }
}
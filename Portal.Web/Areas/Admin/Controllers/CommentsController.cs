﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Controllers;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CommentsController : BaseController<Comment, ICommentRepository>
{
    public CommentsController(UnitOfWork uow, ILogger<BaseController<Comment, ICommentRepository>> logger, ICommentRepository repository) : base(uow, logger, repository)
    {
    }
}

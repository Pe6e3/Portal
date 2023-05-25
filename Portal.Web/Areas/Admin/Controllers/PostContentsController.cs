using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PostContentsController : BaseController<PostContent, IPostContentRepository>
{
    public PostContentsController(UnitOfWork unitOfWork, ILogger<BaseController<PostContent, IPostContentRepository>> logger, IPostContentRepository repository) : base(unitOfWork,logger, repository)
    {
    }
}

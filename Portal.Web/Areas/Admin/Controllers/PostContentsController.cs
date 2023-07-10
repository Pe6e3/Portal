using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Controllers;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
    [Authorize(Roles = "1,2")]
public class PostContentsController : BaseController<PostContent, IPostContentRepository>
{
    public PostContentsController(UnitOfWork uow, ILogger<BaseController<PostContent, IPostContentRepository>> logger, IPostContentRepository repository) : base(uow,logger, repository)
    {
    }
}

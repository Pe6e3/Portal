using AutoMapper;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Areas.Admin.Controllers;

namespace Portal.Web.Controllers;

public class CommentsController : BaseController<Comment, ICommentRepository>
{
    protected new readonly ILogger<BaseController<Comment, ICommentRepository>> logger;
    private readonly UnitOfWork uow;
    private readonly IMapper mapper;


    public CommentsController(UnitOfWork uow, ILogger<BaseController<Comment, ICommentRepository>> logger, ICommentRepository repository, IMapper mapper)
             : base(uow, logger, repository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uow = uow;
    }

 
}

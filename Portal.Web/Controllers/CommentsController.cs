using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async Task<IActionResult> AddComment(string Message, string postSlug, string commentatorName)
    {
        Post commentedPost = await uow.PostRep.GetPostBySlug(postSlug);

        Comment comment = new Comment()
        {
            CreatedAt = DateTime.Now,
            TextComment = Message,
            PostId = commentedPost.Id,
        };

        if (commentatorName != null)
        {
            comment.UserId = await uow.UserRep.GetDefaultUserId();
            comment.TextComment = $"({commentatorName}): {comment.TextComment}";
        }
        else
        {
            User user = await uow.UserRep.GetUserByLogin(User.Identity.Name);
            comment.UserId = user.Id;
        }

        await uow.CommentRep.InsertAsync(comment);

        PostComment postComment = new PostComment()
        {
            PostId = commentedPost.Id,
            CommentId = comment.Id
        };

        await uow.PostCommentRep.InsertAsync(postComment);

        return RedirectToAction("Post", "Categories", new { postSlug = postSlug});


    }

}

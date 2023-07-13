using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

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

        Comment comment = new Comment() // Создаем коммент с текущей датой, передаем в него текст из представления и привязываем к ID поста
        {
            CreatedAt = DateTime.Now,
            TextComment = Message,
            PostId = commentedPost.Id,
        };

        if (commentatorName != null) // Если пользователь не авторизован - добавляем указанное в представлении Имя в текст сообщения, а в ID пользователя ставим пользователя по умолчанию
        {
            comment.UserId = await uow.UserRep.GetDefaultUserId();
            comment.TextComment = $"({commentatorName}): {comment.TextComment}";
        }
        else
        {
            User user = await uow.UserRep.GetUserByLogin(User.Identity.Name); // если пользователь авторизован - записываем его ID 
            comment.UserId = user.Id;
        }

        await uow.CommentRep.InsertAsync(comment);

        commentedPost.Content.CommentsNum++; // Увеличиваем счетчик комментариев у поста
        await uow.PostContentRep.UpdateAsync(commentedPost.Content);


        return RedirectToAction("Post", "Categories", new { postSlug = postSlug });


    }



}

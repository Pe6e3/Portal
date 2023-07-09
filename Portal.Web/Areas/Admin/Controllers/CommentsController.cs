using Microsoft.AspNetCore.Authorization;
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
    private readonly UnitOfWork uow;

    public CommentsController(UnitOfWork uow, ILogger<BaseController<Comment, ICommentRepository>> logger, ICommentRepository repository) : base(uow, logger, repository)
    {
        this.uow = uow;
    }

    public override async Task<IActionResult> Index()
    {
        IEnumerable<Comment> comments = await uow.CommentRep.ListAllComments();
        return View(comments.OrderByDescending(x => x.Id));
    }


    public async Task<IActionResult> DeleteComment(int commentId)
    {
        Comment comment = await uow.CommentRep.GetCommentById(commentId); // получаем коммент, который будем удалять
        Post commentedPost = comment.Post; // получаем откомментированный Пост
        commentedPost.Content.CommentsNum--; // Увеличиваем счетчик комментариев у Поста
        await uow.CommentRep.DeleteAsync(comment);
        await uow.PostContentRep.UpdateAsync(commentedPost.Content);
        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> EditComment(Comment comment)
    {
        if (ModelState.IsValid)
            await uow.CommentRep.UpdateAsync(comment);
        return RedirectToAction("Index");
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Areas.Admin.Controllers;
using Portal.Web.Models;
using Portal.Web.ViewModels;
using System.Diagnostics;
namespace Portal.Web.Controllers;
public class CategoriesController : BaseController<Category, ICategoryRepository>
{
    private readonly UnitOfWork uow;

    public CategoriesController(UnitOfWork uow, ILogger<BaseController<Category, ICategoryRepository>> logger, ICategoryRepository repository) : base(uow, logger, repository)
    {
        this.uow = uow;
    }

    [HttpGet("/category/{*categorySlug}")]
    public async Task<IActionResult> CategoryListIndex(string categorySlug)
    {
        List<PostCategory> postCats = await uow.CategoryRep.GetPostsByCatSlugAsync(categorySlug);

        ViewBag.LastPosts = await uow.PostRep.ListAllAsync(5, "Content");
        return View("Index", postCats);
    }


    [HttpGet("/posts/{*postSlug}")]
    public async Task<IActionResult> Post( string postSlug)
    {
        Post? post = await uow.PostRep.GetPostBySlugAsync(postSlug);
        PostViewModel postViewModel = new PostViewModel();

        postViewModel.Slug = post.Slug;
        postViewModel.CreatedAt = post.CreatedAt;
        postViewModel.Id = post.Id;
        postViewModel.Title = post.Content.Title;
        postViewModel.PostBody = post.Content.PostBody;
        postViewModel.PostImage = post.Content.PostImage;
        postViewModel.PostVideo = post.Content.PostVideo;
        postViewModel.CommentsClosed = post.Content.CommentsClosed;
        postViewModel.Category = await uow.PostCategoryRep.GetCatByPostId(post.Id);
        postViewModel.Comments = await uow.CommentRep.GetCommentsByPostSlug(post.Id);
        return View(postViewModel);
    }



}

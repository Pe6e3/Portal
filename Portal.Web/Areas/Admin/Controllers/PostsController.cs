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
using Portal.Web.ViewModels;
using NuGet.Protocol.Core.Types;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PostsController : BaseController<Post, IPostRepository>
{

    protected readonly ILogger<BaseController<Post, IPostRepository>> _logger;
    //protected readonly IPostRepository _repository;
    private readonly UnitOfWork _uow;

    public PostsController(UnitOfWork unitOfWork, ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository)
        : base(unitOfWork, logger, repository)
    {
        _logger = logger;
        //_repository = repository;
        _uow = unitOfWork;
    }

    public override async Task<IActionResult> Index()
    {
        List<Post> allPosts = (List<Post>)await _uow.PostRep.ListAllAsync();
        List<PostContent> allContent = (List<PostContent>)await _uow.PostContentRep.ListAllAsync();
        List<PostViewModel> posts = new List<PostViewModel>();

        foreach (Post post in allPosts)
        {
            PostContent? content = allContent.FirstOrDefault(x => x.PostId == post.Id);
            posts.Add(new PostViewModel()
            {
                Slug = post.Slug,
                CreatedAt = DateTime.Now,
                PostId = post.Id,
                Title = content.Title,
                PostBody = content.PostBody,
                PostImage = content.PostImage,
                PostVideo = content.PostVideo,
                CommentsClosed = content.CommentsClosed
            });
        }
        return View(posts);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost(PostViewModel postViewModel)
    {
        if (ModelState.IsValid)
        {
            Post post = new Post();
            post.Slug = postViewModel.Slug;
            post.CreatedAt = postViewModel.CreatedAt;

            await _uow.PostRep.InsertAsync(post);

            PostContent content = new PostContent();
            content.PostId = post.Id;
            content.Title = postViewModel.Title;
            content.PostBody = postViewModel.PostBody;
            content.PostImage = postViewModel.PostImage;
            content.CommentsClosed = postViewModel.CommentsClosed;
            content.PostVideo = postViewModel.PostVideo;

            await _uow.PostContentRep.InsertAsync(content);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        Post post = await _uow.PostRep.GetByIdAsync(id);
        PostContent content = await _uow.PostContentRep.GetContentByPostIdAsync(id);
        PostViewModel editPost = new PostViewModel();

        editPost.Slug = post.Slug;
        editPost.CreatedAt = DateTime.Now;
        editPost.PostId = post.Id;
        editPost.Title = content.Title;
        editPost.PostBody = content.PostBody;
        editPost.PostImage = content.PostImage;
        editPost.PostVideo = content.PostVideo;
        editPost.CommentsClosed = content.CommentsClosed;

        return View(editPost);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PostViewModel postViewModel)
    {
        Post post = new Post();
        post.Slug = postViewModel.Slug;
        await _uow.PostRep.InsertAsync(post);

        PostContent content = new PostContent();
        content.Title = postViewModel.Title;
        content.PostBody = postViewModel.PostBody;
        content.PostImage = postViewModel.PostImage;
        content.PostVideo = postViewModel.PostVideo;
        content.CommentsClosed = postViewModel.CommentsClosed;

        await _uow.PostContentRep.InsertAsync(content);

        return RedirectToAction(nameof(Index));
    }

}
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
    protected readonly IPostRepository _repository;
    private readonly UnitOfWork _uow;

    public PostsController(UnitOfWork unitOfWork, ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository)
        : base(unitOfWork, logger, repository)
    {
        _logger = logger;
        _repository = repository;
        _uow = unitOfWork;
    }

    //public override async Task<IActionResult> Index()
    //{
    //    List<Post> allPosts = (List<Post>)await _repository.ListAllAsync();
    //    List<PostContent> allPostsContent = (List<PostContent>)await _repository.ListAllAsync();


    //    List<PostViewModel> post = new List<PostViewModel>();
    //    foreach (var onePost in allPosts)
    //    {
    //        PostContent onePostsContent = allPostsContent.First(x => x.PostId == onePost.Id);
    //        post.Add(new PostViewModel()
    //        {
    //            Id = onePost.Id,
    //            Slug = onePost.Slug,
    //            Title = onePostsContent.Title,
    //            PostBody = onePostsContent.PostBody,
    //            PostImage = onePostsContent.PostImage,
    //            CommentsClosed = onePostsContent.CommentsClosed,
    //        }
    //        );
    //    }

    //    return View(post);
    //}


}
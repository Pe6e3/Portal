using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.ViewModels;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PostsController : BaseController<Post, IPostRepository>
{

    protected new readonly ILogger<BaseController<Post, IPostRepository>> _logger;
    private readonly UnitOfWork _uow;

    public PostsController(UnitOfWork unitOfWork, ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository)
        : base(unitOfWork, logger, repository)
    {
        _logger = logger;
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
                CreatedAt = post.CreatedAt,
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

            // Если поле со ссылкой на ютуб не пустое, то удалить все симовлы с первого по последний "/"
            string postVideo = postViewModel.PostVideo ?? "";  // Исходная ссылка
            int lastSlashIndex = postVideo.LastIndexOf("/");   // позиция последнего слеша
            content.PostVideo = lastSlashIndex != -1 ? postVideo.Substring(lastSlashIndex + 1) : postVideo; //Удаляем все до последнего слеша

            await _uow.PostContentRep.InsertAsync(content);
        }

        return RedirectToAction(nameof(Index));
    }

    [Area("Admin")]
    public override async Task<IActionResult> Update(int id)
    {
        Post post = await _uow.PostRep.GetByIdAsync(id);
        PostContent content = await _uow.PostContentRep.GetContentByPostIdAsync(id);
        PostViewModel editPost = new PostViewModel();

        editPost.Slug = post.Slug;
        editPost.CreatedAt = post.CreatedAt;
        editPost.PostId = post.Id;
        editPost.Title = content.Title;
        editPost.PostBody = content.PostBody;
        editPost.PostImage = content.PostImage;
        editPost.PostVideo = content.PostVideo;
        editPost.CommentsClosed = content.CommentsClosed;

        return View(editPost);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Area("Admin")]
    public async Task<IActionResult> UpdatePVM(PostViewModel postViewModel)
    {
        if (ModelState.IsValid)
        {
            Post post = await _uow.PostRep.GetByIdAsync(postViewModel.PostId);

            post.Slug = postViewModel.Slug;

            await _uow.PostRep.UpdateAsync(post);

            // Получим Пост контент
            PostContent postContent = await _uow.PostContentRep.GetContentByPostIdAsync(postViewModel.PostId);

            // Заполняем Пост контент из формы
            postContent.Title = postViewModel.Title;
            postContent.PostBody = postViewModel.PostBody;
            postContent.CommentsClosed = postViewModel.CommentsClosed;
            postContent.PostImage = postViewModel.PostImage;

              // Если поле со ссылкой на ютуб не пустое, то удалить все симовлы с первого по последний "/"
            string postVideo = postViewModel.PostVideo ?? "";  // Исходная ссылка
            int lastSlashIndex = postVideo.LastIndexOf("/");   // позиция последнего слеша
            postContent.PostVideo = lastSlashIndex != -1 ? postVideo.Substring(lastSlashIndex + 1) : postVideo; //Удаляем все до последнего слеша


            await _uow.PostContentRep.UpdateAsync(postContent);

            return RedirectToAction(nameof(Index));
        }
        return View(postViewModel);
    }

    public override async Task<IActionResult> Details(int id)
    {
        Post post = await _uow.PostRep.GetByIdAsync(id);
        PostContent content = await _uow.PostContentRep.GetContentByPostIdAsync(id);
        PostViewModel postViewModel = new PostViewModel();

        postViewModel.Slug = post.Slug;
        postViewModel.CreatedAt = post.CreatedAt;
        postViewModel.PostId = post.Id;
        postViewModel.Title = content.Title;
        postViewModel.PostBody = content.PostBody;
        postViewModel.PostImage = content.PostImage;
        postViewModel.PostVideo = content.PostVideo;
        postViewModel.CommentsClosed = content.CommentsClosed;

        return View(postViewModel);
    }

}





using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.ViewModels;
using System.Text.Json;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PostsController : BaseController<Post, IPostRepository>
{

    protected new readonly ILogger<BaseController<Post, IPostRepository>> logger;
    private readonly UnitOfWork uow;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IMapper mapper;

    public PostsController(UnitOfWork uow, ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        : base(uow, logger, repository)
    {
        this.logger = logger;
        this.uow = uow;
        this.webHostEnvironment = webHostEnvironment;
        this.mapper = mapper;
    }

    public override async Task<IActionResult> Index()
    {
        List<Post> allPosts = (List<Post>)await uow.PostRep.ListAllAsync();
        List<PostContent> allContent = (List<PostContent>)await uow.PostContentRep.ListAllAsync();
        List<PostViewModel> posts = new List<PostViewModel>();

        mapper.Map(allPosts, posts);
        mapper.Map(allContent, posts);

        return View(posts);
    }

    [HttpGet]
    public override async Task<IActionResult> Create()
    {
        ViewBag.AllCategories = await uow.CategoryRep.ListAllAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost(PostViewModel postViewModel)
    {
        if (ModelState.IsValid)
        {
            Post post = new Post();
            mapper.Map(postViewModel, post);
            post.CreatedBy = await uow.UserRep.GetUserByLogin(User.Identity.Name);

            await uow.PostRep.InsertAsync(post);

            PostContent content = new PostContent();
            content.PostId = post.Id;
            mapper.Map(postViewModel, content);
            content.PostImage = await ProcessUploadImage(postViewModel, "img/uploads/Posts");

            // Если поле со ссылкой на ютуб не пустое, то удалить все симовлы с первого по последний "/"
            string postVideo = postViewModel.PostVideo ?? "";  // Исходная ссылка
            int lastSlashIndex = postVideo.LastIndexOf("/");   // позиция последнего слеша
            content.PostVideo = lastSlashIndex != -1 ? postVideo.Substring(lastSlashIndex + 1) : postVideo; //Удаляем все до последнего слеша

            await uow.PostContentRep.InsertAsync(content);


            if (postViewModel.CategoriesId != null)
                foreach (int catId in postViewModel.CategoriesId)
                {
                    PostCategory pc = new PostCategory()
                    {
                        PostId = post.Id,
                        CategoryId = catId
                    };
                    await uow.PostCategoryRep.InsertAsync(pc);
                }


        }

        return RedirectToAction(nameof(Index));
    }



    [Area("Admin")]
    public async Task<IActionResult> UpdateGet(int id) /*postId*/
    {
        Post post = await uow.PostRep.GetByIdAsync(id);
        PostContent content = await uow.PostContentRep.GetContentByPostIdAsync(id);
        PostViewModel editPost = new PostViewModel();

        editPost.Slug = post.Slug;
        editPost.CreatedAt = post.CreatedAt;
        editPost.Id = post.Id;
        editPost.Title = content.Title;
        editPost.PostBody = content.PostBody;
        editPost.PostImage = content.PostImage;
        editPost.PostVideo = content.PostVideo;
        editPost.CommentsClosed = content.CommentsClosed;
        /*if(content.PostImage!=null) */
        ViewBag.oldImage = content.PostImage;
        ViewBag.AllCategories = await uow.CategoryRep.ListAllAsync();
        ViewBag.PostCategories = await uow.PostCategoryRep.GetCategoryPosts(id);/*postId*/
        return View("Update", editPost);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Area("Admin")]
    public async Task<IActionResult> UpdatePVM(PostViewModel postViewModel, string? oldImage)
    {
        if (ModelState.IsValid)
        {
            Post post = await uow.PostRep.GetByIdAsync(postViewModel.Id);

            post.Slug = postViewModel.Slug;

            await uow.PostRep.UpdateAsync(post);

            // Получим Пост контент
            PostContent postContent = await uow.PostContentRep.GetContentByPostIdAsync(postViewModel.Id);

            // Заполняем Пост контент из формы
            postContent.Title = postViewModel.Title;
            postContent.PostBody = postViewModel.PostBody;
            postContent.CommentsClosed = postViewModel.CommentsClosed;

            // Если поле со ссылкой на ютуб не пустое, то удалить все симовлы с первого по последний "/"
            string postVideo = postViewModel.PostVideo ?? "";  // Исходная ссылка
            int lastSlashIndex = postVideo.LastIndexOf("/");   // позиция последнего слеша
            postContent.PostVideo = lastSlashIndex != -1 ? postVideo.Substring(lastSlashIndex + 1) : postVideo; //Удаляем все до последнего слеша

            if (postViewModel.ImageFile != null)
            {
                string? newImage = await ProcessUploadImage(postViewModel, "img/uploads/Posts");
                postContent.PostImage = newImage;

                if (oldImage != null) ProcessDeleteImage(oldImage, "\\img\\uploads\\Posts");
            }
            await uow.PostContentRep.UpdateAsync(postContent);

            await uow.PostCategoryRep.DeletePCbyPostIdAsync(post.Id);
            if (postViewModel.CategoriesId != null)
                foreach (int catId in postViewModel.CategoriesId)
                {
                    PostCategory pc = new PostCategory()
                    {
                        PostId = post.Id,
                        CategoryId = catId
                    };
                    await uow.PostCategoryRep.InsertAsync(pc);
                }

            return RedirectToAction(nameof(Index));
        }
        return View(postViewModel);
    }



    public override async Task<IActionResult> Details(int id)
    {
        Post? post = await uow.PostRep.GetByIdAsync(id, "Content", "CreatedBy");
        PostContent? postContent = post.Content;
        PostViewModel postVM = new PostViewModel();

        mapper.Map(post, postVM);        // Добавление в PostViewModel данных из класса Post 
        mapper.Map(postContent, postVM); // Добавление в PostViewModel данных из класса PostContent 


        postVM.CreatedBy = await uow.UserRep.GetUserByLogin(post.CreatedBy.Login);
        return View(postVM);
    }

    private async Task<string?> ProcessUploadImage(PostViewModel postViewModel, string folder)
    {
        string uniqueImageName = "";

        if (postViewModel.ImageFile != null)
        {
            string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
            string fileName = Path.GetFileNameWithoutExtension(postViewModel.ImageFile.FileName); //  Имя файла без расширения
            string fileExtansion = Path.GetExtension(postViewModel.ImageFile.FileName);// Расширение с точкой (.jpg)
            uniqueImageName = fileName + ". " + DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss.ff") + fileExtansion;// задаем уникальное имя чтобы случайно не совпало с чьим-то другим            
            string path = Path.Combine(wwwRootPath, folder, uniqueImageName); // задаем путь к файлу
            using (var fileStream = new FileStream(path, FileMode.Create)) // создаем файл по указанному пути
            {
                await postViewModel.ImageFile.CopyToAsync(fileStream); // копируем в него файл, который загрузили из формы
            }
        }
        return uniqueImageName;
    }

    private void ProcessDeleteImage(string oldImage, string folder)
    {
        string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
        string path = Path.Combine(wwwRootPath + folder + oldImage);
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);
    }

    public async Task<IActionResult> GenerateRandomPosts(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {

            Post post = new Post();
            PostContent content = new PostContent();
            PostCategory postCat = new PostCategory();
            Random random = new Random();

            post.Slug = await GetRandomWords(1);
            post.CreatedAt = DateTime.Now;
            post.CreatedBy = await uow.UserRep.GetUserByLogin(User.Identity.Name);
            await uow.PostRep.InsertAsync(post);

            Category randomCat = await uow.CategoryRep.RandomCatId();
            postCat.PostId = post.Id;
            postCat.CategoryId = randomCat.Id;

            content.Title = post.Id + ". (" + randomCat.Name + ") " + await GetRandomWords(3);
            content.PostBody = await GetRandomWords(50);
            content.PostImage = random.Next(1, 32).ToString() + ".jpg"; // надо добавить в папку uploads изображения с названием 1.jpg, 2.jpg  и так далее попорядку. Количество поставить в random.Next (у меня 32)
            content.PostId = post.Id;

            await uow.PostCategoryRep.InsertAsync(postCat);
            await uow.PostContentRep.InsertAsync(content);

        }
        return RedirectToAction("Index");
    }

    static async Task<string> GetRandomWords(int count)
    {
        string[] words = new string[count];
        string space = count > 1 ? " " : "";
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://random-word-api.herokuapp.com/word?number={count}";
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                words = JsonSerializer.Deserialize<string[]>(jsonResponse);
            }
            else
                throw new Exception("Failed to retrieve random words.");
        }
        return string.Join(space, words);

    }
}





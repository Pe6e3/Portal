﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Controllers;
using Portal.Web.ViewModels;
using System.Data;
using System.Text.Json;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "1,2")]
public class PostsController : BaseController<Post, IPostRepository>
{

    protected new readonly ILogger<BaseController<Post, IPostRepository>> logger;
    private readonly UnitOfWork uow;
    private readonly IMapper mapper;
    private readonly IWebHostEnvironment webHostEnvironment;


    public PostsController(UnitOfWork uow, ILogger<BaseController<Post, IPostRepository>> logger, IPostRepository repository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        : base(uow, logger, repository)
    {
        this.logger = logger;
        this.uow = uow;
        this.mapper = mapper;
        this.webHostEnvironment = webHostEnvironment;

    }

    public override async Task<IActionResult> Index()
    {
        List<Post> allPosts = (List<Post>)await uow.PostRep.ListAllDesc("Content");
        List<PostViewModel> postsVM = new List<PostViewModel>();
        mapper.Map(allPosts, postsVM);

        return View(postsVM.OrderByDescending(x => x.PostId));
    }

    public async Task<IActionResult> IndexPosts()
    {
        var posts = await uow.PostRep.ListAllDesc("Content");
        return View(posts);
    }


    [HttpGet]
    public override async Task<IActionResult> Create()
    {
        ViewBag.AllCategories = await uow.CategoryRep.ListAll();
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
            post.CategoryId = postViewModel.CategoriesId[0];

            await uow.PostRep.InsertAsync(post);

            PostContent content = new PostContent();
            mapper.Map(postViewModel, content);
            content.PostId = post.Id;
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



    public async Task<IActionResult> DeletePost(int postId)
    {
        await uow.PostCategoryRep.DeletePCbyPostId(postId);
        await uow.PostContentRep.DeleteContentByPostId(postId);
        await uow.PostRep.DeletePostById(postId);
        return RedirectToAction("Index");
    }



    [Area("Admin")]
    public async Task<IActionResult> UpdateGet(int id) /*postId*/
    {
        Post post = await uow.PostRep.GetByIdAsync(id);
        PostContent content = await uow.PostContentRep.GetContentByPostIdAsync(id);
        PostViewModel editPost = new PostViewModel();

        editPost.Slug = post.Slug;
        editPost.CreatedAt = post.CreatedAt;
        editPost.PostId = post.Id;
        editPost.Title = content.Title;
        editPost.PostBody = content.PostBody;
        editPost.PostImage = content.PostImage;
        editPost.PostVideo = content.PostVideo;
        editPost.CommentsClosed = content.CommentsClosed;
        /*if(content.PostImage!=null) */
        ViewBag.oldImage = content.PostImage;
        ViewBag.AllCategories = await uow.CategoryRep.ListAll();
        ViewBag.PostCategories = await uow.PostCategoryRep.GetCategoryPosts(postId: id);
        return View("Update", editPost);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Area("Admin")]
    public async Task<IActionResult> UpdatePVM(PostViewModel postViewModel, string? oldImage)
    {
        if (ModelState.IsValid)
        {
            Post post = await uow.PostRep.GetByIdAsync(postViewModel.PostId);

            post.Slug = postViewModel.Slug;

            await uow.PostRep.UpdateAsync(post);

            // Получим Пост контент
            PostContent postContent = await uow.PostContentRep.GetContentByPostIdAsync(postViewModel.PostId);

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

            await uow.PostCategoryRep.DeletePCbyPostId(post.Id);
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
        PostViewModel postVM = new PostViewModel();

        mapper.Map(post, postVM);        // Добавление в PostViewModel данных из класса Post 


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

    public async Task<IActionResult> GenerateRandomPosts(int count = 1, int catId = 0)
    {
        for (int i = 0; i < count; i++)
        {

            Post post = new Post();
            PostContent content = new PostContent();
            PostCategory postCat = new PostCategory();

            Category? category = new Category();
            if (catId == 0) category = await uow.CategoryRep.RandomCatId(); // Если категория не задана - выбираем случайную
            else category = await uow.CategoryRep.GetByIdAsync(catId);       // Если задана - выбираем определенную

            Random random = new Random();

            post.Slug = await GetRandomWords(1);
            //post.Slug = random.Next(1, 1000).ToString();

            post.CreatedAt = DateTime.Now;
            post.CreatedBy = await uow.UserRep.GetUserByLogin(User.Identity.Name);
            post.CategoryId = category.Id;
            await uow.PostRep.InsertAsync(post);

            postCat.PostId = post.Id;
            postCat.CategoryId = category.Id;

            //content.Title = post.Id + ". (" + category.Name + ") Random Title";
            content.Title = post.Id + ". (" + category.Name + ") " + await GetRandomWords(random.Next(2, 6));
            //content.PostBody = "Body text";
            content.PostBody = await GetRandomWords(random.Next(150, 350));
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
            string apiUrl = $"https://random-word-api.herokuapp.com/word?number={count}"; // получаем по API случайное английское слово
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                words = JsonSerializer.Deserialize<string[]>(jsonResponse); // добавляем слово из Json ответа в строку
            }
            else
                throw new Exception("Failed to retrieve random words.");
        }

        Random random = new Random();
        for (int i = 1; i < words.Length; i++)
        {
            if (random.NextDouble() <= 0.05 && i > 6) // с вероятностью 5% и не раньше 7го слова делаем новый абзац и заглавную букву
            {
                words[i - 1] += ".<br />";
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
            }
            else if (random.NextDouble() <= 0.1) // с вероятностью 10% ставим запятую, если перед этим не было нового абзаца
                words[i - 1] += ",";
            else if (random.NextDouble() <= 0.03) // с вероятностью 3% ставим тире, если перед этим не было нового абзаца
                words[i - 1] += " - ";
        }

        words[0] = char.ToUpper(words[0][0]) + words[0].Substring(1); // Первая буква - всегда заглавная
        return string.Join(space, words);
    }

}





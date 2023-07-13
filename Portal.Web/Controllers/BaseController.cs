using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Controllers
{

    public abstract class BaseController<TEntity, TRepository> : Controller
        where TEntity : class
        where TRepository : IGenericRepositoryAsync<TEntity>
    {
        protected readonly ILogger<BaseController<TEntity, TRepository>> _logger;
        protected readonly TRepository _repository;
        private readonly UnitOfWork _uow;


        protected BaseController(UnitOfWork uow, ILogger<BaseController<TEntity, TRepository>> logger, TRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _uow = uow;
        }

        public virtual async Task<IActionResult> Index() =>
            View(await _repository.ListAll());

        public virtual async Task<IActionResult> Details(int id) =>
            View(await _repository.GetByIdAsync(id));

        [HttpGet]
        public virtual async Task<IActionResult> Create() =>
            View();



        public virtual async Task<IActionResult> Edit(int id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return View(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        public virtual async Task<IActionResult> Delete(int id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            if (entity != null)
                await _repository.DeleteAsync(entity);

            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _repository.InsertAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public virtual IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });



        //public async Task<string?> ProcessUploadIMG(IFormFile imageFile, string folder)
        //{
        //    string uniqueIMGName = "";

        //    if (imageFile != null)
        //    {
        //        string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
        //        string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName); //  Имя файла без расширения
        //        string fileExtansion = Path.GetExtension(imageFile.FileName);// Расширение с точкой (.jpg)
        //        uniqueIMGName = fileName + ". " + DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss") + fileExtansion;// задаем уникальное имя чтобы случайно не совпало с чьим-то другим            
        //        string path = Path.Combine(wwwRootPath, folder, uniqueIMGName); // задаем путь к файлу
        //        using (var fileStream = new FileStream(path, FileMode.Create)) // создаем файл по указанному пути
        //        {
        //            await imageFile.CopyToAsync(fileStream); // копируем в него файл, который загрузили из формы
        //        }
        //    }
        //    return uniqueIMGName; // возвращаем имя файла с расширением ("myImage.01.01.2020 10:15:59.jpg")
        //}

        //public void ProcessDeleteIMG(string oldImage, string folder)
        //{
        //    string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
        //    string path = Path.Combine(wwwRootPath + folder + oldImage);
        //    if (System.IO.File.Exists(path))
        //        System.IO.File.Delete(path);
        //}

    }

}

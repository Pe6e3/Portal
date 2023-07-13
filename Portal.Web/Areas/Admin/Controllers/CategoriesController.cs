using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Areas.Admin.Controllers.ViewModels;
using Portal.Web.Controllers;
using Portal.Web.Models;
using Portal.Web.ViewModels;
using System.Data;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "1,2")]
public class CategoriesController : BaseController<Category, ICategoryRepository>
{
    private readonly UnitOfWork uow;
    private readonly ILogger<BaseController<Category, ICategoryRepository>> logger;
    private readonly ICategoryRepository repository;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IMapper mapper;

    public CategoriesController(UnitOfWork uow, ILogger<BaseController<Category, ICategoryRepository>> logger, ICategoryRepository repository, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(uow, logger, repository)
    {
        this.uow = uow;
        this.logger = logger;
        this.repository = repository;
        this.webHostEnvironment = webHostEnvironment;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        Category category = await uow.CategoryRep.GetByIdAsync(id);
        CategoryViewModel categoryVM = new  CategoryViewModel();
        if (category != null)
        {
            categoryVM = mapper.Map(category, categoryVM);
            return View("Edit", categoryVM);
        }
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> UpdateCategory(CategoryViewModel newCategory)
    {
        Category oldCategory = await uow.CategoryRep.GetCategoryBySlugAsync(newCategory.Slug);
        if (newCategory.CategoryImageFile != null)
        {
            if (oldCategory.CategoryImage != null)
                ProcessDeleteImage(oldCategory.CategoryImage, "\\img\\uploads\\Categories");
            oldCategory.CategoryImage = await ProcessUploadImage(newCategory, "img/uploads/Categories");
        }
        oldCategory.Id = newCategory.Id;
        if (oldCategory.Slug != newCategory.Slug) oldCategory.Slug = newCategory.Slug;
        if (oldCategory.Description != newCategory.Description) oldCategory.Description = newCategory.Description;
        if (oldCategory.Name != newCategory.Name) oldCategory.Name = newCategory.Name;

        await uow.CategoryRep.UpdateAsync(oldCategory);

        return RedirectToAction("Index");
    }


    private async Task<string?> ProcessUploadImage(CategoryViewModel categoryVM, string folder)
    {
        string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
        string fileName = Path.GetFileNameWithoutExtension(categoryVM.CategoryImageFile.FileName); //  Имя файла без расширения
        string fileExtansion = Path.GetExtension(categoryVM.CategoryImageFile.FileName);// Расширение с точкой (.jpg)
        string uniqueImageName = fileName + ". (" + DateTime.Now.ToString("dd.MM.yy HH-mm-ss") + ")" + fileExtansion;// задаем уникальное имя чтобы случайно не совпало с чьим-то другим            
        string path = Path.Combine(wwwRootPath, folder, uniqueImageName); // задаем путь к файлу
        using (var fileStream = new FileStream(path, FileMode.Create)) // создаем файл по указанному пути
        {
            await categoryVM.CategoryImageFile.CopyToAsync(fileStream); // копируем в него файл, который загрузили из формы
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


}

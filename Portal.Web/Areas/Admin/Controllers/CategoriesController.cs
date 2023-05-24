using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
//public class CategoriesController : Controller
//{
//    private readonly ILogger<CategoriesController> _logger;
//    private readonly UnitOfWork _uow;


//    public CategoriesController(ILogger<CategoriesController> logger, UnitOfWork unitOfWork)
//    {
//        _logger = logger;
//        _uow = unitOfWork;
//    }
//    public async Task<IActionResult> Index() => View(await _uow.CategoryRep.ListAllAsync());

//    public async Task<IActionResult> Details(int id) => View(await _uow.CategoryRep.GetByIdAsync(id));
//    public async Task<IActionResult> Create() => View();

//    public async Task<IActionResult> Delete(int id)
//    {
//        Category category = await _uow.CategoryRep.GetByIdAsync(id);
//        if (category != null)
//            await _uow.CategoryRep.DeleteAsync(category);

//        return RedirectToAction(nameof(Index));
//    }


//    [HttpPost]
//    public async Task<IActionResult> Create(Category category)
//    {
//        if (ModelState.IsValid)
//        {
//            await _uow.CategoryRep.InsertAsync(category);
//            return RedirectToAction(nameof(Index));
//        }
//        return View(category);
//    }


//    public async Task<IActionResult> Update(int id)
//    => !(await _uow.CategoryRep.GetByIdAsync(id) is Category category)
//    ? NotFound() : View(category);

//    //public async Task<IActionResult> Update(int id)
//    //{
//    //    Category category = await _uow.CategoryRep.GetByIdAsync(id);
//    //    if (category == null)
//    //        return NotFound();
//    //    return View(category);
//    //}


//    [HttpPost]
//    public async Task<IActionResult> Update(Category category)
//    {
//        if (ModelState.IsValid)
//        {
//            await _uow.CategoryRep.UpdateAsync(category);
//            return RedirectToAction(nameof(Index));

//        }
//        return View(category);
//    }


//    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


public class CategoriesController : BaseController<Category, CategoryRepository>
{
    public CategoriesController(ILogger<BaseController<Category, CategoryRepository>> logger, CategoryRepository repository)
        : base(logger, repository)
    {
    }
}

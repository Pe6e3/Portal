using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.BLL.Repositories;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Models;
using Portal.Web.ViewModels;
using System.Diagnostics;

namespace Portal.Web.Areas.Admin.Controllers
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

        public virtual async Task<IActionResult> Index() => View(await _repository.ListAllAsync());

        public virtual async Task<IActionResult> Details(int id) => View(await _repository.GetByIdAsync(id));

        [HttpGet]
        public virtual async Task<IActionResult> Create() => View();



        public virtual async Task<IActionResult> Update(int id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return View(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TEntity entity)
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
    }

}

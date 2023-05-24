using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.Web.Models;
using System.Diagnostics;

namespace Portal.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly UnitOfWork _uow;
      

        public CategoriesController(ILogger<CategoriesController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _uow = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CategoryRep.ListAllAsync());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

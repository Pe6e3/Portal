using Microsoft.AspNetCore.Mvc;
using Portal.DAL.Entities;
using Portal.Web.ViewModels;

namespace Portal.Web.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(LoginViewModel lvm)
		{
			return View();
		}


		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel lvm)
		{
			return View();
		}


		public async Task<IActionResult> Authenticate(User user)
		{

			return View();
		}


		public IActionResult Logout()
		{
			return View();
		}





	}
}

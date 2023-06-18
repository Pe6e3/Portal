using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Enum;
using Portal.Web.ViewModels;
using System.Security.Claims;

namespace Portal.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UnitOfWork uow;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(UnitOfWork uow, IWebHostEnvironment webHostEnvironment)
        {
            this.uow = uow;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                if (!await uow.UserRep.UserCheck(lvm.Login, lvm.Email)) // TODO: проверяем, нет ли в базе пользователя с таким же логином или почтой.  (Как пользователь узнает, если уже есть?)
                {
                    if (lvm.Password == lvm.PasswordConfirm) // TODO: нужно вернуться обратно в форму регистрации и сообщить, что пароли не совпадают
                    {
                        User user = new User();
                        user.Login = lvm.Login;
                        user.Password = uow.UserRep.HashPass(lvm.Password);
                        user.RoleId = (int)RoleName.User;
                        await uow.UserRep.InsertAsync(user);

                        UserProfile profile = new UserProfile();
                        profile.Firstname = lvm.Firstname;
                        profile.Lastname = lvm.Lastname;
                        profile.Birthday = lvm.Birthday;
                        profile.Email = lvm.Email;
                        string? newImage = await ProcessUploadAvatar(lvm, "img/uploads/Profiles/");
                        profile.AvatarImg = newImage;
                        profile.UserId = user.Id;
                        profile.RegistrationDate = lvm.RegistrationDate;
                        await uow.UserProfileRep.InsertAsync(profile);

                        await Authenticate(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(lvm);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User loginUser)
        {
            if (ModelState.IsValid)
            {
                User user = await uow.UserRep.ValidateUser(loginUser.Login, loginUser.Password);
                if (user == null) ModelState.AddModelError("", "Нет такого пользователя");
                else if (user.Password == "") ModelState.AddModelError("", "Неверный пароль");
                else
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }


        public async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                });
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        public async Task<string?> ProcessUploadAvatar(LoginViewModel profile, string folder = "img/uploads/Profiles/")
        {
            string uniqueAvatarName = "";

            if (profile.AvatarFile != null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
                string fileName = Path.GetFileNameWithoutExtension(profile.AvatarFile.FileName); //  Имя файла без расширения
                string fileExtansion = Path.GetExtension(profile.AvatarFile.FileName);// Расширение с точкой (.jpg)
                uniqueAvatarName = fileName + ". " + DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss.ff") + fileExtansion;// задаем уникальное имя чтобы случайно не совпало с чьим-то другим            
                string path = Path.Combine(wwwRootPath, folder, uniqueAvatarName); // задаем путь к файлу
                using (var fileStream = new FileStream(path, FileMode.Create)) // создаем файл по указанному пути
                {
                    await profile.AvatarFile.CopyToAsync(fileStream); // копируем в него файл, который загрузили из формы
                }
            }
            return uniqueAvatarName;
        }

        private void ProcessDeleteAvatar(string oldImage, string folder = "img/uploads/Profiles/")
        {
            string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
            string path = Path.Combine(wwwRootPath + folder + oldImage);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }


        public async Task<IActionResult> Profile(string login)
        {
            User user = await uow.UserRep.GetUserByLogin(login);
            ProfileViewModel profileVM = new ProfileViewModel();
            profileVM.Firstname = user.Profile.Firstname;
            profileVM.Lastname = user.Profile.Lastname;
            profileVM.Email = user.Profile.Email;

            return View(profileVM);
        }

    }
}

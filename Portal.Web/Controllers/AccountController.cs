﻿using Microsoft.AspNetCore.Authentication;
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
                if (!await uow.UserRep.UserCheck(lvm.Login)) // TODO: проверяем, нет ли в базе пользователя с таким же логином.  (Как пользователь узнает, если уже есть?)
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
                        string? newImage = await ProcessUploadAvatar(lvm.AvatarFile, "img/uploads/Profiles/");
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
                // TODO: Посмотреть здесь как делается передача ошибки
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


        public async Task<string?> ProcessUploadAvatar(IFormFile imageFile, string folder = "img/uploads/Profiles/")
        {
            string uniqueAvatarName = "";

            if (imageFile != null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath; // путь к корневой папке wwwroot
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName); //  Имя файла без расширения
                string fileExtansion = Path.GetExtension(imageFile.FileName);// Расширение с точкой (.jpg)
                uniqueAvatarName = fileName + ". " + DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss.ff") + fileExtansion;// задаем уникальное имя чтобы случайно не совпало с чьим-то другим            
                string path = Path.Combine(wwwRootPath, folder, uniqueAvatarName); // задаем путь к файлу
                using (var fileStream = new FileStream(path, FileMode.Create)) // создаем файл по указанному пути
                {
                    await imageFile.CopyToAsync(fileStream); // копируем в него файл, который загрузили из формы
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
            login = string.IsNullOrEmpty(login) ? User.Identity.Name : login;
            User user = await uow.UserRep.GetUserByLogin(login);
            ProfileViewModel profileVM = new ProfileViewModel();
            profileVM.Firstname = user.Profile.Firstname;
            profileVM.Lastname = user.Profile.Lastname;
            profileVM.Email = user.Profile.Email;
            profileVM.Birthday = user.Profile.Birthday;
            profileVM.Login = login;
            profileVM.RegistrationDate = user.Profile.RegistrationDate;
            profileVM.AvatarImg = user.Profile.AvatarImg;
            profileVM.Role = user.Role;

            return View(profileVM);
        }

        public async Task<IActionResult> SaveProfile(ProfileViewModel profileVM, bool fromAdmin = false)
        {
            User user = await uow.UserRep.GetUserByLogin(profileVM.Login);
            UserProfile profile = await uow.UserProfileRep.GetByIdAsync(user.Profile.Id);

            if (profileVM.AvatarFile != null) profile.AvatarImg = await ProcessUploadAvatar(profileVM.AvatarFile);
            if (profile.Firstname != profileVM.Firstname) profile.Firstname = profileVM.Firstname;
            if (profile.Lastname != profileVM.Lastname) profile.Lastname = profileVM.Lastname;
            if (profile.Birthday != profileVM.Birthday) profile.Birthday = profileVM.Birthday;
            if (profile.Email != profileVM.Email) profile.Email = profileVM.Email;



            if (fromAdmin /* && User.IsInRole("1") */)
            {
                if (user.Role.Id != profileVM.RoleId) user.RoleId = profileVM.RoleId;
                await uow.UserProfileRep.UpdateAsync(profile);
                return RedirectToAction("Index", "Users", new { area = "Admin", login = user.Login });
            }

            await uow.UserProfileRep.UpdateAsync(profile);
            return RedirectToAction("Profile", "Account", new { login = user.Login });
        }


    }
}

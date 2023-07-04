using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Plugins;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.ViewModels;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : BaseController<User, IUserRepository>
{
    private readonly UnitOfWork uow;
    private readonly ILogger<BaseController<User, IUserRepository>> logger;
    private readonly IUserRepository repository;
    private readonly IMapper mapper;

    public UsersController(UnitOfWork uow, ILogger<BaseController<User, IUserRepository>> logger, IUserRepository repository, IMapper mapper) : base(uow, logger, repository)
    {
        this.uow = uow;
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }


    public override async Task<IActionResult> Index()
    {
        IEnumerable<User> users = await uow.UserRep.GetAllUsers();
        IEnumerable<ProfileViewModel> profiles = users.Select(user =>
        {
            var profileViewModel = mapper.Map<ProfileViewModel>(user);
            profileViewModel.Firstname = user.Profile?.Firstname;
            profileViewModel.Lastname = user.Profile?.Lastname;
            profileViewModel.Email = user.Profile?.Email;
            profileViewModel.RegistrationDate = user.Profile?.RegistrationDate;
            profileViewModel.Birthday = user.Profile?.Birthday;
            profileViewModel.AvatarImg = user.Profile?.AvatarImg;
            return profileViewModel;
        });

        return View(profiles);
    }


    public async Task<IActionResult> EditUser(string login)
    {
        User user = await uow.UserRep.GetUserByLogin(login);
        ProfileViewModel profileVM = new ProfileViewModel();
        mapper.Map(user, profileVM);
        mapper.Map(user.Profile, profileVM);
        profileVM.Role = user.Role;

        ViewBag.Roles = await uow.RoleRep.ListAllAsync();
        return View("Edit", profileVM);
    }

 


}

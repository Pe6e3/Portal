using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components
{
    [ViewComponent(Name = "_Profile")]
    public class ProfileViewComponent : ViewComponent
    {
        private readonly UnitOfWork uow;

        public ProfileViewComponent(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IViewComponentResult> InvokeAsync() /* передает авторизованного юзера в представление Default/_Profile */
        {
            if (User.Identity.IsAuthenticated)
            {
                string login = User.Identity.Name;
                return View(await uow.UserRep.GetUserByLogin(login));
            }
            User user = await uow.UserRep.GetDefaultUser();
            return View(user);
        }

    }
}

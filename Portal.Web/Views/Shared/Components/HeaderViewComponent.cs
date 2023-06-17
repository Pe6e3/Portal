using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components
{
    [ViewComponent(Name = "_TopMenu")]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UnitOfWork uow;

        public HeaderViewComponent(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IViewComponentResult> InvokeAsync() /*передает список пунктов меню в представление Default/_TopMenu*/
        {
            List<MenuItem> menuItem = await uow.MenuItemRep.GetByMenuIdAsync(1);
            return View(menuItem);
        }

    }
}

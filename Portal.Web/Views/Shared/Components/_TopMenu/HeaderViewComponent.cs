using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;

namespace Portal.Web.Views.Shared.Components._TopMenu
{
    [ViewComponent(Name = "_TopMenu")]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UnitOfWork _uow;

        public HeaderViewComponent(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IViewComponentResult> InvokeAsync() /*передает список пунктов меню в представление Default/_TopMenu*/
        {
            List<MenuItem> menuItem = await _uow.MenuItemRep.GetByMenuIdAsync(1);
            return View(menuItem);
        }

    }
}

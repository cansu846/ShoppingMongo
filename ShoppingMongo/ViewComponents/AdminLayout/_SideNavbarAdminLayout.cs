using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.AdminLayout
{
    public class _SideNavbarAdminLayout:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

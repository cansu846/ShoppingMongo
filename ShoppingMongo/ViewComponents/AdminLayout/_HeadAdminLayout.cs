using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.AdminLayout
{
    public class _HeadAdminLayout:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

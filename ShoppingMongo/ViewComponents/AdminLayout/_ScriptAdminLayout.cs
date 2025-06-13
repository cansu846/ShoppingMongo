using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.AdminLayout
{
    public class _ScriptAdminLayout:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();  
        }
    }
}

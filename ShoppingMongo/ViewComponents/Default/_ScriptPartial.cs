using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _ScriptPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _HeaderPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

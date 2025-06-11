using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _MenuPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

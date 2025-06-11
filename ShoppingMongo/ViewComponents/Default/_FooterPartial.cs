using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _FooterPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _SliderPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

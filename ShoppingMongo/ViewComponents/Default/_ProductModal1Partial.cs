using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Services.CategoryService;
using ShoppingMongo.Services.ProductDetailImageService;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _ProductModal1Partial:ViewComponent
    {
        private readonly IProductDetailImageService _productDetailImageService;

        public _ProductModal1Partial(IProductDetailImageService productDetailImageService)
        {
            _productDetailImageService = productDetailImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list =await  _productDetailImageService.GetAllProductDetailImageAsync();
            return View(list);
        }

    }
}

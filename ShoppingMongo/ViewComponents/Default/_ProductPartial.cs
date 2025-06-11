using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Services.CategoryService;
using ShoppingMongo.Services.ProductService;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _ProductPartial:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public _ProductPartial(IProductService productService,
            ICategoryService categoryService)
        {
            _productService = productService;   
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values=await _productService.GetAllProductWithCategoryAsync();
            ViewBag.categories = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }

    }
}

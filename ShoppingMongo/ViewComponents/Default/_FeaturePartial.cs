using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Services.CategoryService;

namespace ShoppingMongo.ViewComponents.Default
{
    public class _FeaturePartial:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public _FeaturePartial(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.ProductDtos;
using ShoppingMongo.Services.ProductService;

namespace ShoppingMongo.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IProductService _productService;

        public DefaultController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByCategory(string categoryId)
        {
            List<ResultProductDto> products;

            if (string.IsNullOrEmpty(categoryId))
            {
                products=await _productService.GetAllProductAsync();
            }
            else
            {
                products = await _productService.GetProductByCategoryName(categoryId);
            }
            return Json(products);
        }
    }
}

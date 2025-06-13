using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.ProdcutDetailImageDtos;
using ShoppingMongo.Services.ProductDetailImageService;

namespace ShoppingMongo.Controllers
{
    public class ProductDetailImageController : Controller
    {
        private readonly IProductDetailImageService _productDetailImageService;

        public ProductDetailImageController(IProductDetailImageService productDetailImageService)
        {
            _productDetailImageService = productDetailImageService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _productDetailImageService.GetAllProductDetailImageAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProductDetailImage()
        {
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> CreateProductDetailImage(CreateProductDetailImageDto createProductDetailImageDto)
        {
            await _productDetailImageService.CreateProductDetailImageAsync(createProductDetailImageDto);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteProductDetailImage(string id)
        {
            await _productDetailImageService.DeleteProductDetailImageAsync(id);
            return RedirectToAction("Index");
        }
    }
}

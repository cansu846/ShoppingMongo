using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.ProductDtos;
using ShoppingMongo.Services.CategoryService;
using ShoppingMongo.Services.ProductService;

namespace ShoppingMongo.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService; 
        private readonly IMapper _mapper;
        public DefaultController(IProductService productService,
            ICategoryService categoryService,   
            IMapper mapper)
        {
            _productService = productService;
             _categoryService = categoryService;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(); 
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsByCategoryNameSearch(string searchString)
        {
            var products = await _productService.GetAllProductWithCategoryAsync();// tüm ürünler
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                products = products
                    .Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(searchString))
                    .ToList();
            }
            var productDto = _mapper.Map<List<ResultProductDto>>(products);
            ViewBag.categories = await _categoryService.GetAllCategoryAsync();
            ViewData["currentFilter"] = searchString;
            return PartialView("~/Views/Shared/Components/_ProductPartial/Default.cshtml", productDto); // sadece ürün HTML'si döner
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Dtos.ProductDtos;
using ShoppingMongo.Services.CategoryService;
using ShoppingMongo.Services.ProductService;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace ShoppingMongo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService,
            ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _productService.GetAllProductWithCategoryAsync();
            return View(values);
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            //Razor Pages projelerinde dropdown listesi (açılır menü) oluşturmak için kullanılır.
            //SelectListItem: ASP.NET MVC'de <select> öğeleri için kullanılan bir sınıftır. Her öğe iki temel özelliğe sahiptir:
            //Text: Kullanıcıya görünen metin
            //Value: Seçildiğinde sunucuya gönderilecek değer
            //.ToList(): LINQ sorgusunu çalıştırır ve sonucu liste haline getirir.
            ViewBag.v = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId
            }).ToList();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteProduct(string id)
        {
            _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.v = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId
            }).ToList();
            var exsistProdcut = await _productService.GetProductByIdAsync(id);
            return View(exsistProdcut);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("Index");
        }

    }
}

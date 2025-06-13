using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Services.CategoryService;

namespace ShoppingMongo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto.CategoryName != null && createCategoryDto.CategoryImage != null)
            {

                try
                {

                    await _categoryService.CreateCategoryAsync(createCategoryDto);
                    return Json(new { success = true, message = "Category added successfully" });

                }
                catch (Exception ex)
                {
                    return Json(new { success = true, message = "Error: " + ex.Message });
                }

            }
            return View(createCategoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            //seweet alert
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> UpdateCategory(string id)
        {
            var exisitCategory = await _categoryService.GetCategoryByIdAsync(id);
            return View(exisitCategory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(updateCategoryDto);
                return Json(new { success = true, message = "Category updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

    }
}

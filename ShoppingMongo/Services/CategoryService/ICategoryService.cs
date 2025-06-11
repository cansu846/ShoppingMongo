using ShoppingMongo.Dtos.CategoryDtos;

namespace ShoppingMongo.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task DeleteCategoryAsync(string id);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id);
        Task<string> GetCategoryNameByIdAsync(string id);
    }
}

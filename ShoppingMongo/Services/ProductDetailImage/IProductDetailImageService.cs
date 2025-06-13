using ShoppingMongo.Dtos.ProdcutDetailImageDtos;

namespace ShoppingMongo.Services.ProductDetailImageService
{
    public interface IProductDetailImageService
    {
        Task<List<ResultProductDetailImageDto>> GetAllProductDetailImageAsync();
        Task CreateProductDetailImageAsync(CreateProductDetailImageDto createProductDetailImageDto);
        Task DeleteProductDetailImageAsync(string id);
        Task UpdateProductDetailImageAsync(UpdateProductDetailImageDto updateProductDetailImageDto);
        Task<GetProductDetailImageDto> GetProductDetailImageByIdAsync(string id);
    }
}

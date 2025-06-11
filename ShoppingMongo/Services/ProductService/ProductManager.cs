using AutoMapper;
using MongoDB.Driver;
using ShoppingMongo.Dtos.ProductDtos;
using ShoppingMongo.Entities;
using ShoppingMongo.Services.CategoryService;
using ShoppingMongo.Settings;

namespace ShoppingMongo.Services.ProductService
{
    public class ProductManager : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly ICategoryService _categoryService;
        public ProductManager(IMapper mapper,
            IDatabaseSettings databaseSettings,
            ICategoryService categoryService)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            var collection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _productCollection = collection;
            _mapper = mapper;

            _categoryService = categoryService;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            if (createProductDto.ImageUrlFormFile != null && createProductDto.ProductName.Length > 0)
            {
                var fileName = Path.GetFileName(createProductDto.ImageUrlFormFile.FileName);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cozastore-master/images", fileName);

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await createCategoryDto.CategoryImage.CopyToAsync(stream);
                //}

                //dosya adını veritabanında tutar
                createProductDto.ImageUrl = "/cozastore-master/images/" + fileName;
                createProductDto.Status = true;
            }
            var value = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(p => p.ProductId == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<List<ResultProductDto>> GetAllProductWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            var result = new List<ResultProductDto>();
            foreach (var product in values)
            {
                var resultProductDto = _mapper.Map<ResultProductDto>(product);
                //var temp = await _categoryService.GetCategoryNameByIdAsync(product.CategoryId)
                resultProductDto.CategoryName = await _categoryService.GetCategoryNameByIdAsync(product.CategoryId);
                result.Add(resultProductDto);
            }
            return result;
        }

        public async Task<List<ResultProductDto>> GetProductByCategoryName(string categoryId)
        {
            var values = await _productCollection.Find(x => x.CategoryId == categoryId).ToListAsync();
            var result = _mapper.Map<List<ResultProductDto>>(values);
            return result;
        }

        public async Task<GetProductByIdDto> GetProductByIdAsync(string id)
        {
            var value = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetProductByIdDto>(value);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.ImageUrlFormFile != null && updateProductDto.ProductName.Length > 0)
            {
                var fileName = Path.GetFileName(updateProductDto.ImageUrlFormFile.FileName);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cozastore-master/images", fileName);

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await createCategoryDto.CategoryImage.CopyToAsync(stream);
                //}

                //dosya adını veritabanında tutar
                updateProductDto.ImageUrl = "/cozastore-master/images/" + fileName;
            }
            var value = _mapper.Map<Product>(updateProductDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, value);
        }
    }
}

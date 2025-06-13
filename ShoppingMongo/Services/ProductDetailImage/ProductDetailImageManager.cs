using AutoMapper;
using MongoDB.Driver;
using ShoppingMongo.Dtos.ProdcutDetailImageDtos;
using ShoppingMongo.Entities;
using ShoppingMongo.Settings;
using System.Collections.Generic;

namespace ShoppingMongo.Services.ProductDetailImageService
{
    public class ProductDetailImageManager : IProductDetailImageService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductDetailImage> _productDetailImageCollection;

        public ProductDetailImageManager(IMapper mapper,
            IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            var collection = database.GetCollection<ProductDetailImage>(databaseSettings.ProductDetailImageName);
            _productDetailImageCollection = collection;
            _mapper = mapper;
        }

        public async Task CreateProductDetailImageAsync(CreateProductDetailImageDto dto)
        {
            var fileList = new List<IFormFile>
    {
        dto.ImageUrlFromFile1,
        dto.ImageUrlFromFile2,
        dto.ImageUrlFromFile3
    };

            var urlList = new List<string>();

            for (int i = 0; i < fileList.Count; i++)
            {
                var file = fileList[i];
                var fileName = Path.GetFileName(file.FileName);
                var relativePath = $"cozastore-master/images/product_detail_images/{fileName}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                urlList.Add(relativePath);
            }

            // DTO'ya yolları yerleştir
            dto.ImageUrl1 = urlList.ElementAtOrDefault(0);
            dto.ImageUrl2 = urlList.ElementAtOrDefault(1);
            dto.ImageUrl3 = urlList.ElementAtOrDefault(2);

            var productDetailImage = _mapper.Map<ProductDetailImage>(dto);
            await _productDetailImageCollection.InsertOneAsync(productDetailImage);
        }


        public async Task DeleteProductDetailImageAsync(string id)
        {
            await _productDetailImageCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultProductDetailImageDto>> GetAllProductDetailImageAsync()
        {
            //find(true) sarta baglı olamdan tum elemanları listeler
            var values = await _productDetailImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDetailImageDto>>(values);
        }

        public async Task<GetProductDetailImageDto> GetProductDetailImageByIdAsync(string id)
        {
            //firstordefaultasync, MongoDB veritabanında sorgu sonucunda eşleşen ilk dökümana ulaşmak için kullanılan bir metottur.
            //Eğer hiçbir döküman eşleşmezse, null döner.
            var ProductDetailImage = await _productDetailImageCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<GetProductDetailImageDto>(ProductDetailImage);
        }

        public async Task UpdateProductDetailImageAsync(UpdateProductDetailImageDto updateProductDetailImageDto)
        {
            if (updateProductDetailImageDto.ImageUrl1 != null && updateProductDetailImageDto.ImageUrl1.Length > 0)
            {
                var fileName = Path.GetFileName(updateProductDetailImageDto.ImageUrlFromFile1.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cozastore-master/images/product_detail_images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateProductDetailImageDto.ImageUrlFromFile1.CopyToAsync(stream);
                }

                //dosya adını veritabanında tutar
                updateProductDetailImageDto.ImageUrl1 = "cozastore-master/images/product_detail_images/" + fileName;
            }
            //buradaki ?; Null koşullu erişim (null-conditional operator)
            //Eğer _mapper null değilse, Map<ProductDetailImage>(...) metodunu çağır.
            //Eğer _mapper null ise, bu satırı sessizce atla ve null döndür.
            var ProductDetailImage = _mapper?.Map<ProductDetailImage>(updateProductDetailImageDto);
            await _productDetailImageCollection.FindOneAndReplaceAsync(x => x.Id == updateProductDetailImageDto.Id, ProductDetailImage);
        }
    }
}

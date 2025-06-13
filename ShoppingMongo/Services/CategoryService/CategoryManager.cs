using AutoMapper;
using MongoDB.Driver;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Entities;
using ShoppingMongo.Settings;

namespace ShoppingMongo.Services.CategoryService
{
    public class CategoryManager : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryManager(IMapper mapper,
            IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            var collection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _categoryCollection = collection;   
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto.CategoryImage != null && createCategoryDto.CategoryImage.Length > 0)
            {
                var fileName = Path.GetFileName(createCategoryDto.CategoryImage.FileName);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cozastore-master/images", fileName);

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await createCategoryDto.CategoryImage.CopyToAsync(stream);
                //}

                //dosya adını veritabanında tutar
                createCategoryDto.CategoryImagePath = "/cozastore-master/images/" + fileName;
            }
            //var category = _mapper.Map<Category>(createCategoryDto);    
            var category = new Category { 
                CategoryName = createCategoryDto.CategoryName,
                CategoryImagePath = createCategoryDto.CategoryImagePath,    
                CategoryDescription = createCategoryDto.CategoryDescription
            };
            await _categoryCollection.InsertOneAsync(category);   
        }

        public async Task DeleteCategoryAsync(string id)
        {
           await _categoryCollection.DeleteOneAsync(x=>x.CategoryId==id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            //find(true) sarta baglı olamdan tum elemanları listeler
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
        {
            //firstordefaultasync, MongoDB veritabanında sorgu sonucunda eşleşen ilk dökümana ulaşmak için kullanılan bir metottur.
            //Eğer hiçbir döküman eşleşmezse, null döner.
            var category = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetCategoryByIdDto>(category);   
        }

        public async Task<string> GetCategoryNameByIdAsync(string id)
        {
            var value = await _categoryCollection.Find(x=>x.CategoryId == id).FirstOrDefaultAsync();
            return value?.CategoryName ?? "Category not found";
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto.CategoryImage != null && updateCategoryDto.CategoryImage.Length > 0)
            {
                var fileName = Path.GetFileName(updateCategoryDto.CategoryImage.FileName);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cozastore-master/images", fileName);

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await createCategoryDto.CategoryImage.CopyToAsync(stream);
                //}

                //dosya adını veritabanında tutar
                updateCategoryDto.CategoryImagePath = "/cozastore-master/images/" + fileName;
            }
            //buradaki ?; Null koşullu erişim (null-conditional operator)
            //Eğer _mapper null değilse, Map<Category>(...) metodunu çağır.
            //Eğer _mapper null ise, bu satırı sessizce atla ve null döndür.
            //var category = _mapper?.Map<Category>(updateCategoryDto);
            var category = new Category
            {
                CategoryId = updateCategoryDto.CategoryId,  
                CategoryName = updateCategoryDto.CategoryName,
                CategoryImagePath = updateCategoryDto.CategoryImagePath,
                CategoryDescription = updateCategoryDto.CategoryDescription
            };
            await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryId==updateCategoryDto.CategoryId, category);
        }
    }
}

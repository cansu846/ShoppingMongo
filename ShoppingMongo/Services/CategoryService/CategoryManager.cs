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

        public CategoryManager(Mapper mapper,
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
            var category = _mapper.Map<Category>(createCategoryDto);    
            await _categoryCollection.InsertOneAsync(category);   
        }

        public async Task DeleteCategoryAsync(string id)
        {
           await _categoryCollection.DeleteOneAsync(id);
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
            var category = _categoryCollection.Find(x=>x.CategoryId==id)
            return _mapper.Map<GetCategoryByIdDto>(category);   
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            //buradaki ?; Null koşullu erişim (null-conditional operator)
            //Eğer _mapper null değilse, Map<Category>(...) metodunu çağır.
            //Eğer _mapper null ise, bu satırı sessizce atla ve null döndür.
            var category = _mapper?.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryId==updateCategoryDto.CategoryId, category);
        }
    }
}

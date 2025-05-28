using AutoMapper;
using MongoDB.Driver;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Entities;
using ShoppingMongo.Settings;

namespace ShoppingMongo.Services.CustomerService
{
    public class CustomerManager:ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerManager(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            //Veri tabanı baglantı adresi alınır
            var clientName = new MongoClient(databaseSettings.ConnectionString);
            //Veritabanı ismi alınır
            var database = clientName.GetDatabase(databaseSettings.DatabaseName);
            _customerCollection = database.GetCollection<Customer>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var value = _mapper.Map<Customer>(createCustomerDto);
            await _customerCollection.InsertOneAsync(value);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(id);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var values = await _customerCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCustomerDto>>(values);
        }

        public async Task<GetCustomerByIdDto> GetCustomerByIdAsync(string id)
        {
            var value = await _customerCollection.Find(x => x.CustomerId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetCustomerByIdDto>(value);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var value = _mapper.Map<Customer>(_customerCollection);
            await _customerCollection.FindOneAndReplaceAsync(x => x.CustomerId == updateCustomerDto.CustomerId, value);
        }
    }
}

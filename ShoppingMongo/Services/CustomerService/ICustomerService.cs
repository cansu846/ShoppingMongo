using ShoppingMongo.Dtos.CustomerDtos;

namespace ShoppingMongo.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task<GetCustomerByIdDto> GetCustomerByIdAsync(string id);
        Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);
        Task CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task DeleteCustomerAsync(string id);
    }
}

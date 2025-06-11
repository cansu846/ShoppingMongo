using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Services.CustomerService;

namespace ShoppingMongo.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await  _customerService.GetAllCustomerAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCustomer() { 
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            await _customerService.CreateCustomerAsync(createCustomerDto);
            return RedirectToAction("Index");
        }

        [HttpGet]   
        public async Task<IActionResult> DeleteCustomer(string id) {

            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("Index");   
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(string id) { 
            var exsistCustomer = await _customerService.GetCustomerByIdAsync(id);   
            return View(exsistCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            await _customerService.UpdateCustomerAsync(updateCustomerDto);
            return RedirectToAction("Index");
        }
    }
}

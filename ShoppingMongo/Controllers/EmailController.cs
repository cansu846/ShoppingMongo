using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Services.EmailService;

namespace ShoppingMongo.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("E-posta adresi gerekli.");
            }

            var couponCode = GenerateCouponCode(); // örnek: KUPON-XYZ123
            await _emailService.SendDiscountEmailAsync(email, couponCode);
            TempData["SuccessMessage"] = "Indirim kuponu e-posta adresinize gonderildi";

            return RedirectToAction("Index","Default");
        }

        private string GenerateCouponCode()
        {
            return "KUPON-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
        }

    }
}

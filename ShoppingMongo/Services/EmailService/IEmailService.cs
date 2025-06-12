namespace ShoppingMongo.Services.EmailService
{
    public interface IEmailService
    {
        public Task SendDiscountEmailAsync(string toEmail, string couponCode);
    }
}

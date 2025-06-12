namespace ShoppingMongo.Services.EmailService
{
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using ShoppingMongo.Settings;

    public class EmailService:IEmailService
    {
        private readonly IEmailSettings _emailSettings;

        public EmailService(IEmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendDiscountEmailAsync(string toEmail, string couponCode)
        {
            Console.WriteLine("SenderEmail: " + _emailSettings.SenderEmail); // null mı?

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("Müşteri", toEmail)); 
            message.Subject = "🎁 Özel İndirim Kuponun";

            message.Body = new TextPart("html")
            {
                Text = $"<h3>İndirim Kuponunuz:</h3><p><strong>{couponCode}</strong></p>"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}

using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class SendgridEmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        public SendgridEmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send(string email, string subject, string htmlContent)
        {
            var apiKey = _configuration.GetValue<string>("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(email, "test");
            var to = new EmailAddress(email);
            var messageTextPlain = Regex.Replace(htmlContent, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, messageTextPlain, htmlContent);
            var response = await client.SendEmailAsync(msg);
         }
    }
}

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

        public async Task Send(string fromEmail, string toEmail, string subject, string body)
        {
            var apiKey = _configuration.GetValue<string>("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var bodyFromLocal = System.IO.File.ReadAllText(@"..\..\..\..\OngProject\Templates\htmlpage.html");

            bodyFromLocal = bodyFromLocal.Replace("@correoContacto", fromEmail)
                                         .Replace("@bodyEmail", body)
                                         .Replace("@titulo", subject);

            var from = new EmailAddress(fromEmail);

            var to = new EmailAddress(toEmail); 

            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, bodyFromLocal);

            var response = await client.SendEmailAsync(msg);
         }
    }
}

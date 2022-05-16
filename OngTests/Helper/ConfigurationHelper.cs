using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace OngTests.Helper
{
    public class ConfigurationHelper
    {
        public readonly IConfiguration Config;

        public ConfigurationHelper()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:secret", ""},
                {"SqlConnectionString", ""},
                {"MailParams:SendGridKey", ""},
                {"MailParams:FromMail", ""},
                {"MailParams:FromMailDescription", ""},
                {"MailParams:PathTemplate", ""},
                {"MailParams:ReplaceMailTitle", "{mail_title}"},
                {"MailParams:ReplaceMailBody", "{mail_body}"},
                {"MailParams:ReplaceMailContact", "{mail_contact}"},
                {"MailParams:WelcomeMailTitle", "Bienvenidos a Ong Somos Mas!"},
                {"MailParams:WelcomeMailBody", "<p>¡Bienvenidos a Ong Somos Mas!</p>"},
                {"MailParams:WelcomeMailContact", "ONGTeam188@gmail.com"},
                {"SendGridAPIKey",""}
            };

            Config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}

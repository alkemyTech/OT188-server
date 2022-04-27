using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEmailServices
    {
        //Task Send(string email, string subject, string htmlContent);
        //Task Send(string fromEmail, string subject, string body);
        Task Send(string fromEmail, string toEmail, string subject, string body);
    }
}

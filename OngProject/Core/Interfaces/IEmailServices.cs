using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEmailServices
    {
        Task Send(string email, string subject, string htmlContent);
    }
}

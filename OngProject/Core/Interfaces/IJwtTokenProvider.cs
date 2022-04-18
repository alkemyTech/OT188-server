using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IJwtTokenProvider
    {
        public Task<string> CreateJwtToken(User user);
    }
}

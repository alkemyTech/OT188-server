using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IJwtTokenProvider
    {
        public Task<string> CreateJwtToken(User user);
    }
}

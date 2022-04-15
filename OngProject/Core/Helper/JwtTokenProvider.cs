using OngProject.Core.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        public async Task<string> CreateJwtToken(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}

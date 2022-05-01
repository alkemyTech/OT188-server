using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface ISlideRepository : IRepository<Slide>
    {
        Task<int> GetLastOrder();
    }
}

using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class SlideRepository : Repository<Slide>, ISlideRepository
    {
        public SlideRepository(OngProjectDbContext context) : base(context)
        {
        }
        public async Task<int> GetLastOrder()
        {
            return await _entities.MaxAsync(sl => sl.Order);
        }
    }
}

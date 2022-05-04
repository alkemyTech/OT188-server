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
            try
            {
                return await _entities.MaxAsync(sl => sl.Order);
            }
            catch (System.Exception e)
            {
                if (e.Message == "Sequence contains no elements.")
                {
                    return 0;
                }
                throw;
            }
           
        }
    }
}

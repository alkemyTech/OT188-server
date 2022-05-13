using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.DataAccess.SeedsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests.Helper
{
    public class DbContextHelper
    {
        private static OngProjectDbContext _context { get; set; }

        public DbContextHelper()
        {
            
        }

        public static OngProjectDbContext ConfigureDbContext(ModelBuilder modelBuilder, bool useSeeds = true)
        {

            _context = new OngProjectDbContext(modelBuilder);

            if (useSeeds)
            {
                modelBuilder.ApplyConfiguration(new SeedActivities());
                modelBuilder.ApplyConfiguration(new SeedNews());
                modelBuilder.ApplyConfiguration(new SeedUsers());
                modelBuilder.ApplyConfiguration(new SeedRol());
                modelBuilder.ApplyConfiguration(new SeedTestimonials());
                modelBuilder.ApplyConfiguration(new SeedOrganizations());
                modelBuilder.ApplyConfiguration(new SeedCategories());
                modelBuilder.ApplyConfiguration(new SeedComments());
                modelBuilder.ApplyConfiguration(new SeedMembers());
            }

            return _context;
        }
    }
}

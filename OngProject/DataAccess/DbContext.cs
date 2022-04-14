using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.DataAccess
{
    public class OngProjectDbContext : DbContext
    {
        public OngProjectDbContext() { }

        public OngProjectDbContext(DbContextOptions options) : base(options)
        { 

        }

        public DbSet<Member> Members { get; set; }
    }
}

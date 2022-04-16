using Microsoft.EntityFrameworkCore;
using OngProject.Core.Models;
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}

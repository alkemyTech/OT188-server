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
        public DbSet<New> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<User> Users { get;set; }
        public DbSet<Testimony> Testimonials { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

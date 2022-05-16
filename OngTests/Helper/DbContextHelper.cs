using Microsoft.EntityFrameworkCore;
using OngProject.Core.Helper;
using OngProject.DataAccess;
using OngProject.Entities;
using System;

namespace OngTests.Helper
{
    public class DbContextHelper
    {
        private static OngProjectDbContext _context { get; set; }

        public static OngProjectDbContext UseDbContext(bool pupulate = true)
        {
            var options = new DbContextOptionsBuilder<OngProjectDbContext>().UseInMemoryDatabase(databaseName: "Ong").Options;

            _context = new OngProjectDbContext(options);
            _context.Database.EnsureDeleted();

            if (pupulate)
            {
                PrepareRoles();
                PrepareUsers();
                SeedMembers();
                SeedOrganization();
                SeedSlide();
                SeedNews();
                SeedCategory();
                SeedContacts();
                SeedTestimonials();
                _context.SaveChanges();
            }

            return _context;
        }

        private static void PrepareRoles()
        {
            _context.Add(new Rol
            {
                Id = 1,
                Name = "User",
                Description = "Regular user without permissions"
            });
            _context.Add(new Rol
            {
                Id = 2,
                Name = "Administrator",
                Description = "Administrator user with permissions"
            });
        }

        private static void PrepareUsers()
        {
            for (int i = 1; i < 11; i++)
            {
                var delete = i > 1 ? false : true; // el primer usuario lo creo eliminado
                _context.Add(
                    new User
                    {
                        Id = i,
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "User" + i + "@ong.com",
                        Password = EncryptSha256.Encrypt("Password" + i),
                        Photo = "Photo" + i,
                        IsDeleted = delete,
                        RolesId = 1,
                        ModifiedAt = DateTime.Now
                    }
                );
            }

            for (int i = 10; i < 20; i++)
            {
                _context.Add(
                    new User
                    {
                        Id = i,
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "User" + i + "@ong.com",
                        Password = EncryptSha256.Encrypt("Password" + i),
                        Photo = "Photo" + i,
                        IsDeleted = false,
                        RolesId = 2,
                        ModifiedAt = DateTime.Now
                    }
                );
            }
        }

        private static void SeedMembers()
        {
            //agrego este miembro para verificar que no se puede eliminar un miembro ya eliminado
            _context.Add(
                    new Member
                    {
                        Id = 1,
                        Name = "Name " + 1,
                        Image = "Image " + 1,
                        FacebookUrl = "Facebook Url " + 1,
                        InstagramUrl = "Instagram Url " + 1,
                        LinkedinUrl = "Linkedin Url " + 1,
                        Description = "Description " + 1,
                        IsDeleted = true,
                        ModifiedAt = DateTime.Now
                    }
                );
            for (int i = 1; i < 10; i++)
            {
                _context.Add(
                    new Member
                    {
                        Id = i,
                        Name = "Name " + i,
                        Image = "Image " + i,
                        FacebookUrl = "Facebook Url " + i,
                        InstagramUrl = "Instagram Url " + i,
                        LinkedinUrl = "Linkedin Url " + i,
                        Description = "Description " + i,
                        IsDeleted = false,
                        ModifiedAt = DateTime.Now
                    }
                );
            }
        }

        private static void SeedOrganization()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Organization
                    {
                        Id = i,
                        Name = "Organization name " + i,
                        Image = "Image " + i,
                        Address = "Address " + i,
                        Phone = "Phone" + i,
                        Email = "name@organization.com",
                        WelcomeText = "Welcome text " + i,
                        AboutUsText = "About us text " + i,
                        FacebookUrl = "Facebook url " + i,
                        InstagramUrl = "Instagram url " + i,
                        LinkedinUrl = "Linkedin url " + i,
                        IsDeleted = false,
                        ModifiedAt = DateTime.Now
                    }
                );
            }
        }

        public static void SeedSlide()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Slide
                    {
                        Id = i,
                        ImageUrl = "Image " + i,
                        Text = "Text " + i,
                        Order = i,
                        OrganizationId = i,
                        IsDeleted = false,
                        ModifiedAt = DateTime.Now
                    }
               );
            }
        }

        private static void SeedNews()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new New
                    {
                        Id = i,
                        Name = "New " + i,
                        Content = "Content for New " + i,
                        Image = "image_new " + i,
                        ModifiedAt = DateTime.Now,
                        CategoryId = i,
                    }
               );
            }
        }

        private static void SeedCategory()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Category
                    {
                        Id = i,
                        Name = "Category " + i,
                        Description = "Description for Category" + i,
                        Image = "image_category" + i,
                        ModifiedAt = DateTime.Now
                    }
               );
            }
        }

        public static void SeedContacts()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Contact
                    {
                        Id = i,
                        Name = "Contact Name " + i,
                        Phone = i,
                        Email = "name" + i + "@mail.com",
                        Message = "Message " + i,
                        IsDeleted = false,
                        ModifiedAt = DateTime.Now
                    }
                );
            }
        }

        public static void SeedTestimonials()
        {
            for (int i = 1; i < 11; i++)
            {
                _context.Add(
                    new Testimony
                    {
                        Id = i,
                        Name = "Name testimonial " + i,
                        Description = "Description testimonial" + i,
                        Image = "Image testimonial " + i,
                        IsDeleted = false,
                        ModifiedAt = DateTime.Now
                    }
                );
            }
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.SeedsData
{
    public class SeedUsers : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            CreateSeed(builder,"Standard",1,1,11);
            CreateSeed(builder,"Regular",2,11,21);
        }
        private static void CreateSeed(EntityTypeBuilder<User> builder, string type, int rol, int ini, int fin)
        {
            for (int i = ini; i < fin; i++)
            {
                builder.HasData(
                    new User
                    {
                        Id = i,
                        FirstName = "User # " + i,
                        LastName = type,
                        Email = $"User{type}{i}@gmail.com",
                        Password = $"admin{i}",
                        Photo = $"user photo #{i}",
                        RolesId = rol,
                        ModifiedAt = DateTime.Now,
                        IsDeleted = false
                    });
            }
            
        }
    }
}
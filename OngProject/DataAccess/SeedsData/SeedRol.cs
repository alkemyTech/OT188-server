using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.SeedsData
{
    public class SeedRol : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            CreateSeed(builder, "Administrator", 1);
            CreateSeed(builder, "Standard",2);
            CreateSeed(builder, "Regular", 3);
        }
        private static void CreateSeed(EntityTypeBuilder<Rol> builder, string type, int value)
        {
            builder.HasData(
                new Rol
                {
                    Id = value,
                    Name = type,
                    Description = $"{type} rol type",
                    ModifiedAt = DateTime.Now,
                    IsDeleted = false
                });
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess.SeedsData
{
    public class SeedTestimonials : IEntityTypeConfiguration<Testimony>
    {
        public void Configure(EntityTypeBuilder<Testimony> builder)
        {
            for (int i = 1; i < 11; i++)
            {
                builder.HasData(
                    new Testimony
                    {
                        Id = i,
                        Name = "Testimony name " + i,
                        Image = "Testimony image " + i,
                        Description = "Testimony description " + i,
                        ModifiedAt = DateTime.Now.AddDays(i),
                        IsDeleted = false
                    });
            }
        }
    }
}

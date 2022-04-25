using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess.SeedsData
{
    public class SeedOrganizations : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasData(
                new Organization
                {
                    Id = 1,
                    Name = "ONG-SomosMás",
                    Image = "LogoSomosMás",
                    Address = "Su dirección",
                    Phone = "1234567890",
                    Email = "somosmas_ong@gmail.com",
                    WelcomeText = "Welcome text",
                    AboutUsText = "About us text",
                    FacebookUrl = "SomosMasFacebook",
                    InstagramUrl = "SomosMasInstagram",
                    LinkedinUrl = "SomosMasLinkedIn",
                    IsDeleted = false,
                    ModifiedAt = DateTime.Now
                });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.SeedsData
{
    public class SeedMembers : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new Member
					{
						Id = i,
						Name = "NAME-"+i,
						FacebookUrl = "www.facebook.com/NAME-"+i,
						InstagramUrl = "www.instagram.com/NAME-"+i,
						LinkedinUrl = "www.linkedin.com/NAME-"+i,
						Image = "NAME-" + i + "-IMAGE",
						Description = "NAME-" + i + "-DESCRIPTION",
						IsDeleted = false,
						ModifiedAt = System.DateTime.Now
					}
				);
			}
		}
    }
}

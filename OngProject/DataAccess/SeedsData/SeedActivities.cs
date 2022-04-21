using Microsoft.EntityFrameworkCore;
using OngProject.Entities;

namespace OngProject.DataAccess.SeedsData

{
	public class SeedActivities : IEntityTypeConfiguration<Activity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Activity> builder)
        {
			for (int i = 1; i < 10; i++)
			{
				builder.HasData(
					new Activity
					{
						Id = i,
						Name = "Actividad  " + i,
						Content = "El contenido de la actividad Número " + i,
						Image = "Imagen de la actividad " + i,
						ModifiedAt = System.DateTime.Now,						
						IsDeleted = false
					}
				);
			}
		}
    }
}

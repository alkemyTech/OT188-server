using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;

namespace OngProject.DataAccess.SeedsData
{
    public class SeedCategories : IEntityTypeConfiguration<Category>
    {
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new Category
					{
						Id = i,
						Name = "Categoria " + i,
						Description = "descripcion de categoria " + i,
						Image = "Imágenes de categoria " + i,
						ModifiedAt = System.DateTime.Now,
						IsDeleted = false
					}
				);
			}
		}
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
	public class SeedNews : IEntityTypeConfiguration<New>
	{
		public void Configure(EntityTypeBuilder<New> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new New
					{
						Id = i,
						Name = "Novedades " + i,
						Content = "Contenido para Novedades " + i,
						Image = "Imágenes de novedades " + i,
						ModifiedAt = DateTime.Now,
						IsDeleted = false,
						CategoryId = i,
					}
				);
			}
		}
	}
}

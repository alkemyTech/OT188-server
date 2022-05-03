using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
	public class SeedComments : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new Comment
					{
						Id = i,
						Body = "Comentario"+i,
						IdUser = 10+i,
						NewId = i,
						ModifiedAt = DateTime.Now,
						IsDeleted = false,
					}
				);
			}
		}
	}
}
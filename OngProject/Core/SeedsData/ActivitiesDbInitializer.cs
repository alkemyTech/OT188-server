using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OngProject.DataAccess;
using OngProject.Entities;
using System;
using System.Linq;

namespace OngProject.Core.SeedsData
{
    public class ActivitiesDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<OngProjectDbContext>();

                if (!context.Activities.Any())
                {
                    context.Activities.AddRange(new Activity()
                    {
                        Name = "El nombre de la actividad",
                        Content = "La descripcion de la actividad",
                        Image = "http://urlalaimagen.com/imagen1",
                        ModifiedAt = DateTime.Now.AddDays(-10),
                        IsDeleted = false,
                        DeletedAt = DateTime.Now
                    });                   
                    context.SaveChanges();
                }
            }
        }
       
    }
}

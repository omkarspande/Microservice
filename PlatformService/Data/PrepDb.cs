using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace PlatformService.Data{
    public static class PrepDb{
        public static void PrepPopulation(IApplicationBuilder app,bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context,bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> attemting to apply the migrations....");
                try
                {
                    context.Database.Migrate();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"---> count not run the migration : {ex.Message}");
                }
            }

            if(!context.Platforms.Any())
            {
                // Console.WriteLine("--> Seeding data....");
                context.Platforms.AddRange(
                    new Platform(){Name="Dot Net", Publisher="Microsoft", Cost="Free"},
                    new Platform(){Name="SQL Server Express", Publisher="Microsoft", Cost="Free"},
                    new Platform(){Name="KUbernetes", Publisher="Cloud Native Computing Foundation", Cost="Free"}
                );

                context.SaveChanges();
            }
            else
            {
                // Console.WriteLine("--> We already have data");
            }
        }
    }
}
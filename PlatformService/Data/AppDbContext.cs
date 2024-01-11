using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){
            
        }

        public DbSet<Platform> Platforms { get; set; }//This will be used to query the database
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Read_Planet.Models;

namespace Read_Planet.Data
{
    public class ReadPlanetDbContext : IdentityDbContext<AppUser>
    {
        public ReadPlanetDbContext(DbContextOptions<ReadPlanetDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

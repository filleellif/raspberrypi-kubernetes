using Microsoft.EntityFrameworkCore;

namespace Scraper
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasNoKey();

            modelBuilder.Entity<Country>()
                .Ignore(c => c.LatLng);
        }
    }
}

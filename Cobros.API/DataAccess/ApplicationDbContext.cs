using Cobros.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder
                .Entity<RefreshToken>()
                .HasIndex(x => x.Value)
                .IsUnique();
                
            modelBuilder.SeedDB();
        }
    }
}

using Cobros.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Cobros.API.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // Person entities
        public DbSet<User> Users { get; set; }
        public DbSet<DebtCollector> DebtCollectors { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<RefreshToken>()
                .HasIndex(x => x.Value)
                .IsUnique();

            modelBuilder
                .Entity<Person>()
                .ToTable("Persons")
                .HasDiscriminator<int>("PersonType")
                .HasValue<User>(1)
                .HasValue<DebtCollector>(2)
                .HasValue<Customer>(3);

            modelBuilder
                .Entity<Person>()
                .HasIndex(x => x.NationalID)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder
                .Entity<Loan>()
                .HasIndex(x => new { x.CobroId, x.RoutePosition })
                .IsUnique();

            modelBuilder.SeedDB();
        }
    }
}

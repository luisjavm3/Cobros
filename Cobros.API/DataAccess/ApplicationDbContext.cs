using Cobros.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<Cobro> Cobros { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<PartialPayment> PartialPayments { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        // Person entities
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DebtCollector> DebtCollectors { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Cobro>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder
                .Entity<RefreshToken>()
                .HasIndex(x => x.Value)
                .IsUnique();

            modelBuilder
                .Entity<Person>()
                .ToTable("Persons")
                .HasDiscriminator<string>("PersonType")
                .HasValue<User>("user")
                .HasValue<DebtCollector>("debt_collector")
                .HasValue<Customer>("customer");

            modelBuilder
                .Entity<Person>()
                .HasIndex(x => x.NationalID)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            //modelBuilder
            //    .Entity<Loan>()
            //    .HasIndex(x => new { x.CobroId, x.RoutePosition })
            //    .IsUnique();

            modelBuilder
                .Entity<Loan>()
                .HasOne(x=>x.Customer)
                .WithMany(y=>y.Loans)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.SeedDB();
        }
    }
}

using Cobros.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.DataAccess
{
    public static class SeedingData
    {
        public static void SeedDB(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User { Id = 1, Name="User1", LastName="User_Lastname1",Username="User1",Gender=Gender.MALE, Role = Role.USER, PasswordHash=BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 2, Name = "User2", LastName="User_Lastname2",Username="User2",Gender=Gender.FEMALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 3, Name = "User3", LastName="User_Lastname3",Username="User3",Gender=Gender.MALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 4, Name = "User4", LastName="User_Lastname4",Username="User4",Gender=Gender.FEMALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 5, Name = "User5", LastName="User_Lastname5",Username="User5",Gender=Gender.MALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 6, Name = "Admin1", LastName="Admin_Lastname1",Username="Admin1",Gender=Gender.FEMALE, Role = Role.ADMIN, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 7, Name = "Admin2", LastName="Admin_Lastname2",Username="Admin2",Gender=Gender.FEMALE, Role = Role.ADMIN, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")}
                );
        }
    }
}

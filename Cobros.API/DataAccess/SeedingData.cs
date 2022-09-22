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
                    new User { Id = 1, NationalID= "23456", Name="User1", LastName="User_Lastname1",Username="User1",Gender=Gender.MALE, Role = Role.USER, PasswordHash=BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 2, NationalID = "23457", Name = "User2", LastName="User_Lastname2",Username="User2",Gender=Gender.FEMALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 3, NationalID = "23458", Name = "User3", LastName="User_Lastname3",Username="User3",Gender=Gender.MALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 4, NationalID = "23459", Name = "User4", LastName="User_Lastname4",Username="User4",Gender=Gender.FEMALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 5, NationalID = "234560", Name = "User5", LastName="User_Lastname5",Username="User5",Gender=Gender.MALE, Role = Role.USER, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 6, NationalID = "234561", Name = "Admin1", LastName="Admin_Lastname1",Username="Admin1",Gender=Gender.FEMALE, Role = Role.ADMIN, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")},
                    new User { Id = 7, NationalID = "234562", Name = "Admin2", LastName="Admin_Lastname2",Username="Admin2",Gender=Gender.FEMALE, Role = Role.ADMIN, PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")}
                );

            modelBuilder
                .Entity<DebtCollector>()
                .HasData(
                    new DebtCollector { Id = 8, Name = "DebtCollector1", LastName = "DebtCollector1", Gender = Gender.MALE, Address = "DebtCollector1", NationalID = "1234567890" },
                    new DebtCollector { Id = 9, Name = "DebtCollector2", LastName = "DebtCollector2", Gender = Gender.MALE, Address = "DebtCollector2", NationalID = "1234567891" },
                    new DebtCollector { Id = 10, Name = "DebtCollector3", LastName = "DebtCollector3", Gender = Gender.MALE, Address = "DebtCollector3", NationalID = "1234567892" },
                    new DebtCollector { Id = 11, Name = "DebtCollector4", LastName = "DebtCollector4", Gender = Gender.MALE, Address = "DebtCollector4", NationalID = "1234567893" },
                    new DebtCollector { Id = 12, Name = "DebtCollector5", LastName = "DebtCollector5", Gender = Gender.MALE, Address = "DebtCollector5", NationalID = "1234567894" }
                );

            modelBuilder
                .Entity<Cobro>()
                .HasData(
                    new Cobro { Id = 1, Name = "Cobro1", UserId = 1, DebtCollectorId = 8 },
                    new Cobro { Id = 2, Name = "Cobro2", UserId = 2, DebtCollectorId = 9}
                );

            modelBuilder
                .Entity<Customer>()
                .HasData(
                    new Customer { Id = 13, NationalID = "987654321", Name = "Customer1", LastName = "Customer1", Gender = Gender.FEMALE, Address = "Customer1" },
                    new Customer { Id = 14, NationalID = "987654322", Name = "Customer2", LastName = "Customer2", Gender = Gender.FEMALE, Address = "Customer2" },
                    new Customer { Id = 15, NationalID = "987654323", Name = "Customer3", LastName = "Customer3", Gender = Gender.FEMALE, Address = "Customer3" },
                    new Customer { Id = 16, NationalID = "987654324", Name = "Customer4", LastName = "Customer4", Gender = Gender.FEMALE, Address = "Customer4" },
                    new Customer { Id = 17, NationalID = "987654325", Name = "Customer5", LastName = "Customer5", Gender = Gender.FEMALE, Address = "Customer5" },
                    new Customer { Id = 18, NationalID = "987654326", Name = "Customer6", LastName = "Customer6", Gender = Gender.FEMALE, Address = "Customer6" },
                    new Customer { Id = 19, NationalID = "987654327", Name = "Customer7", LastName = "Customer7", Gender = Gender.FEMALE, Address = "Customer7" },
                    new Customer { Id = 20, NationalID = "987654328", Name = "Customer8", LastName = "Customer8", Gender = Gender.FEMALE, Address = "Customer8" },
                    new Customer { Id = 21, NationalID = "987654329", Name = "Customer9", LastName = "Customer9", Gender = Gender.FEMALE, Address = "Customer9" },
                    new Customer { Id = 22, NationalID = "987654330", Name = "Customer10", LastName = "Customer10", Gender = Gender.FEMALE, Address = "Customer10" },
                    new Customer { Id = 23, NationalID = "987654331", Name = "Customer11", LastName = "Customer11", Gender = Gender.FEMALE, Address = "Customer11" },
                    new Customer { Id = 24, NationalID = "987654332", Name = "Customer12", LastName = "Customer12", Gender = Gender.FEMALE, Address = "Customer12" },
                    new Customer { Id = 25, NationalID = "987654333", Name = "Customer13", LastName = "Customer13", Gender = Gender.FEMALE, Address = "Customer13" },
                    new Customer { Id = 26, NationalID = "987654334", Name = "Customer14", LastName = "Customer14", Gender = Gender.FEMALE, Address = "Customer14" },
                    new Customer { Id = 27, NationalID = "987654335", Name = "Customer15", LastName = "Customer15", Gender = Gender.FEMALE, Address = "Customer15" },
                    new Customer { Id = 28, NationalID = "987654336", Name = "Customer16", LastName = "Customer16", Gender = Gender.FEMALE, Address = "Customer16" },
                    new Customer { Id = 29, NationalID = "987654337", Name = "Customer17", LastName = "Customer17", Gender = Gender.FEMALE, Address = "Customer17" },
                    new Customer { Id = 30, NationalID = "987654338", Name = "Customer18", LastName = "Customer18", Gender = Gender.FEMALE, Address = "Customer18" },
                    new Customer { Id = 31, NationalID = "987654339", Name = "Customer19", LastName = "Customer19", Gender = Gender.FEMALE, Address = "Customer19" },
                    new Customer { Id = 32, NationalID = "987654340", Name = "Customer20", LastName = "Customer20", Gender = Gender.FEMALE, Address = "Customer20" }
                );

            modelBuilder
                .Entity<Loan>()
                .HasData(
                    new Loan { Id = 1, CobroId = 1, CustomerId = 13, LoanInterest = 20, RoutePosition = 1, Value = 100, Balance = 115 },
                    new Loan { Id = 2, CobroId = 1, CustomerId = 14, LoanInterest = 20, RoutePosition = 2, Value = 100, Balance =  110},
                    new Loan { Id = 3, CobroId = 1, CustomerId = 15, LoanInterest = 20, RoutePosition = 3, Value = 100, Balance =  105},
                    new Loan { Id = 4, CobroId = 1, CustomerId = 16, LoanInterest = 20, RoutePosition = 4, Value = 100, Balance =  100},
                    new Loan { Id = 5, CobroId = 1, CustomerId = 17, LoanInterest = 20, RoutePosition = 5, Value = 100, Balance =  95},
                    new Loan { Id = 6, CobroId = 1, CustomerId = 18, LoanInterest = 20, RoutePosition = 6, Value = 100, Balance =  90},
                    new Loan { Id = 7, CobroId = 1, CustomerId = 19, LoanInterest = 20, RoutePosition = 7, Value = 100, Balance =  85},
                    new Loan { Id = 8, CobroId = 1, CustomerId = 20, LoanInterest = 20, RoutePosition = 8, Value = 100, Balance =  80},
                    new Loan { Id = 9, CobroId = 1, CustomerId = 21, LoanInterest = 20, RoutePosition = 9, Value = 100, Balance =  75},
                    new Loan { Id = 10, CobroId = 1, CustomerId = 22, LoanInterest = 20, RoutePosition = 10, Value = 100, Balance =  70},
                    new Loan { Id = 11, CobroId = 1, CustomerId = 23, LoanInterest = 20, RoutePosition = 11, Value = 100, Balance =  120},
                    new Loan { Id = 12, CobroId = 1, CustomerId = 24, LoanInterest = 20, RoutePosition = 12, Value = 100, Balance =  120},
                    new Loan { Id = 13, CobroId = 1, CustomerId = 25, LoanInterest = 20, RoutePosition = 13, Value = 100, Balance =  120},
                    new Loan { Id = 14, CobroId = 1, CustomerId = 26, LoanInterest = 20, RoutePosition = 14, Value = 100, Balance =  120},
                    new Loan { Id = 15, CobroId = 1, CustomerId = 27, LoanInterest = 20, RoutePosition = 15, Value = 100, Balance =  120},
                    new Loan { Id = 16, CobroId = 1, CustomerId = 28, LoanInterest = 20, RoutePosition = 16, Value = 100, Balance =  120},
                    new Loan { Id = 17, CobroId = 1, CustomerId = 29, LoanInterest = 20, RoutePosition = 17, Value = 100, Balance =  120},
                    new Loan { Id = 18, CobroId = 1, CustomerId = 30, LoanInterest = 20, RoutePosition = 18, Value = 100, Balance =  120},
                    new Loan { Id = 19, CobroId = 1, CustomerId = 31, LoanInterest = 20, RoutePosition = 19, Value = 100, Balance =  120},
                    new Loan { Id = 20, CobroId = 1, CustomerId = 32, LoanInterest = 20, RoutePosition = 20, Value = 100, Balance =  120}
                );

            modelBuilder
                .Entity<PartialPayment>()
                .HasData(
                    new PartialPayment { LoanId = 1, Value = 5, Id = 1 },
                    new PartialPayment { LoanId = 2, Value = 5, Id = 2 },
                    new PartialPayment { LoanId = 2, Value = 5, Id = 3 },
                    new PartialPayment { LoanId = 3, Value = 5, Id = 4 },
                    new PartialPayment { LoanId = 3, Value = 5, Id = 5 },
                    new PartialPayment { LoanId = 3, Value = 5, Id = 6 },
                    new PartialPayment { LoanId = 4, Value = 5, Id = 7 },
                    new PartialPayment { LoanId = 4, Value = 5, Id = 8 },
                    new PartialPayment { LoanId = 4, Value = 5, Id = 9 },
                    new PartialPayment { LoanId = 4, Value = 5, Id = 10 },
                    new PartialPayment { LoanId = 5, Value = 5, Id = 11 },
                    new PartialPayment { LoanId = 5, Value = 5, Id = 12 },
                    new PartialPayment { LoanId = 5, Value = 5, Id = 13 },
                    new PartialPayment { LoanId = 5, Value = 5, Id = 14 },
                    new PartialPayment { LoanId = 5, Value = 5, Id = 15 },
                    new PartialPayment { LoanId = 6, Value = 5, Id = 16 },
                    new PartialPayment { LoanId = 6, Value = 5, Id = 17 },
                    new PartialPayment { LoanId = 6, Value = 5, Id = 18 },
                    new PartialPayment { LoanId = 6, Value = 5, Id = 20 },
                    new PartialPayment { LoanId = 6, Value = 5, Id = 21 },
                    new PartialPayment { LoanId = 6, Value = 5, Id = 19 },
                    new PartialPayment { LoanId = 7, Value = 5, Id = 22 },
                    new PartialPayment { LoanId = 7, Value = 5, Id = 23 },
                    new PartialPayment { LoanId = 7, Value = 5, Id = 24 },
                    new PartialPayment { LoanId = 7, Value = 5, Id = 25 },
                    new PartialPayment { LoanId = 7, Value = 5, Id = 26 },
                    new PartialPayment { LoanId = 7, Value = 5, Id = 27 },
                    new PartialPayment { LoanId = 7, Value = 5, Id = 28 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 29 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 30 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 31 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 32 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 33 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 34 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 35 },
                    new PartialPayment { LoanId = 8, Value = 5, Id = 36 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 37 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 38 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 39 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 40 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 41 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 42 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 43 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 44 },
                    new PartialPayment { LoanId = 9, Value = 5, Id = 45 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 46 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 47 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 48 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 49 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 50 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 51 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 52 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 53 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 54 },
                    new PartialPayment { LoanId = 10, Value = 5, Id = 55 }
                );
        }
    }
}

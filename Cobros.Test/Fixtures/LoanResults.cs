using Cobros.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobros.Test.Fixtures
{
    public static class LoanResults
    {
        // This is the result when Loan with RoutePosition 18 gets onto RoutePosition 5
        public static IEnumerable<Loan> OrderedLoans = new List<Loan>() {
            new Loan { Id = 1, CobroId = 1, CustomerId = 13, LoanInterest = 20, RoutePosition = 1, Value = 100, Balance = 115 },
            new Loan { Id = 2, CobroId = 1, CustomerId = 14, LoanInterest = 20, RoutePosition = 2, Value = 100, Balance =  110},
            new Loan { Id = 3, CobroId = 1, CustomerId = 15, LoanInterest = 20, RoutePosition = 3, Value = 100, Balance =  105},
            new Loan { Id = 4, CobroId = 1, CustomerId = 16, LoanInterest = 20, RoutePosition = 4, Value = 100, Balance =  100},
            new Loan { Id = 18, CobroId = 1, CustomerId = 30, LoanInterest = 20, RoutePosition = 5, Value = 100, Balance =  120},
            new Loan { Id = 5, CobroId = 1, CustomerId = 17, LoanInterest = 20, RoutePosition = 6, Value = 100, Balance =  95},
            new Loan { Id = 6, CobroId = 1, CustomerId = 18, LoanInterest = 20, RoutePosition = 7, Value = 100, Balance =  90},
            new Loan { Id = 7, CobroId = 1, CustomerId = 19, LoanInterest = 20, RoutePosition = 8, Value = 100, Balance =  85},
            new Loan { Id = 8, CobroId = 1, CustomerId = 20, LoanInterest = 20, RoutePosition = 9, Value = 100, Balance =  80},
            new Loan { Id = 9, CobroId = 1, CustomerId = 21, LoanInterest = 20, RoutePosition = 10, Value = 100, Balance =  75},
            new Loan { Id = 10, CobroId = 1, CustomerId = 22, LoanInterest = 20, RoutePosition = 11, Value = 100, Balance =  70},
            new Loan { Id = 11, CobroId = 1, CustomerId = 23, LoanInterest = 20, RoutePosition = 12, Value = 100, Balance =  120},
            new Loan { Id = 12, CobroId = 1, CustomerId = 24, LoanInterest = 20, RoutePosition = 13, Value = 100, Balance =  120},
            new Loan { Id = 13, CobroId = 1, CustomerId = 25, LoanInterest = 20, RoutePosition = 14, Value = 100, Balance =  120},
            new Loan { Id = 14, CobroId = 1, CustomerId = 26, LoanInterest = 20, RoutePosition = 15, Value = 100, Balance =  120},
            new Loan { Id = 15, CobroId = 1, CustomerId = 27, LoanInterest = 20, RoutePosition = 16, Value = 100, Balance =  120},
            new Loan { Id = 16, CobroId = 1, CustomerId = 28, LoanInterest = 20, RoutePosition = 17, Value = 100, Balance =  120},
            new Loan { Id = 17, CobroId = 1, CustomerId = 29, LoanInterest = 20, RoutePosition = 18, Value = 100, Balance =  120},
            new Loan { Id = 19, CobroId = 1, CustomerId = 31, LoanInterest = 20, RoutePosition = 19, Value = 100, Balance =  120},
            new Loan { Id = 20, CobroId = 1, CustomerId = 32, LoanInterest = 20, RoutePosition = 20, Value = 100, Balance =  120}
        };
    }
}

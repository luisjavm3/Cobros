using Cobros.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobros.Test.Entities
{
    public class LoanTest
    {
        [Theory]
        [InlineData(100, 20, 120)]
        [InlineData(200, 30, 260)]
        [InlineData(150, 10, 165)]
        public void Total_DependsOnValueAndLoanInterest(int value, int loanInterest, double total)
        {
            // Arrange
            var loan = new Loan { Value = value, LoanInterest = loanInterest };

            // Act
            var result = loan.Total;

            Assert.Equal(total, result);
        }
    }
}

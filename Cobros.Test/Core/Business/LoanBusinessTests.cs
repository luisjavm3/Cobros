using Cobros.API.Core.Business;
using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Entities;
using Cobros.Test.Fixtures;
using Microsoft.AspNetCore.Http;

namespace Cobros.Test.Core.Business
{
    public class LoanBusinessTests
    {
        [Fact]
        public async void UpdateRoutePosition()
        {
            //Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var ordered = new List<Loan>() {
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
        }; ;

            httpContextAccessorMock
                .Setup(x => x.HttpContext.Items["User"])
                .Returns(new UserAuthenticatedDto { Role = Role.ADMIN });

            var loanUpdateDto = new LoanUpdateDto { RoutePosition = 5 };
            var loanBusiness = new LoanBusiness(unitOfWork, mapper, httpContextAccessorMock.Object);
            var sut = loanBusiness.UpdateRoutePosition;

            //Act
            await sut(18, loanUpdateDto);

            //Assert
            for (int i = 0; i < 20; i++)
            {
                var position = i+1;
                var inRepository = await unitOfWork.Loans.GetByCobroIdAndRoutePosition(1, position);
                var actual = ordered.SingleOrDefault(x => x.RoutePosition == position);

                actual.Id
                    .Should()
                    .Be(inRepository.Id);
            }
        }
    }
}

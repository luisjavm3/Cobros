using Cobros.API.Entities;
using FluentAssertions;

namespace Cobros.Test.DataAccess
{
    public class ApplicationDbContextTest
    {
        [Fact]
        public async void ApplicationDbContext_WasPopulatedWithTestData()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();

            // Act
            var result = await unitOfWork.Users.GetByIdAsync(1);

            // Assert
            result
                .Should()
                .NotBeNull();

            result
                .Should()
                .BeOfType<User>();

            result
                .Username
                .Should()
                .Be("User1");
        }

    }
}

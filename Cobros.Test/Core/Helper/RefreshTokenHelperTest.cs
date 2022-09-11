using Cobros.API.Core.Helper;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace Cobros.Test.Core.Helper
{
    public class RefreshTokenHelperTest
    {
        [Fact]
        public async void GetUniqueRefreshTokenValue_ReturnsAValue_WhenNotFindingExistingValue()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();

            refreshTokenRepository
                .Setup(x => x.GetByValueAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<RefreshToken>(null)).Verifiable();

            unitOfWorkMock
                .Setup(x=>x.RefreshTokens)
                .Returns(refreshTokenRepository.Object);

            var sut = new RefreshTokenHelper(unitOfWorkMock.Object).GetUniqueRefreshTokenValue;

            // Act
            var result = await sut();

            // Assert
            result
                .Should()
                .NotBeEmpty();

            // Check if GetByValueAsync was executed exactly once.
            refreshTokenRepository
                .Verify(x=>x.GetByValueAsync(It.IsAny<string>()), Times.Exactly(1));
        }
    }
}

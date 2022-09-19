using Cobros.API.Core.Business;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Cobros.Test.Core.Business
{
    public class AuthBusinessTests
    {
        private AuthBusiness GetAuthBusiness()
        {
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration = helper.GetConfiguration();

            return new AuthBusiness(unitOfWork, mapper, configuration);
        }

        [Fact]
        public async void Register_IsSuccessful_WhenUsernameNotExists()
        {
            // Arrange
            AuthRegisterDto authRegisterDto = new() { Username = "not_repeated_username", Password = "123456" };
            var authBusiness = GetAuthBusiness();

            // Act - Assert
            await authBusiness
                .Awaiting(x => x.Register(authRegisterDto))
                .Should()
                .NotThrowAsync();
        }

        [Fact]
        public async void Register_OnSuccess_SavesAUserInDatabase()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration = helper.GetConfiguration();
            AuthRegisterDto toRegister = new() { Username = "not_repeated_username", Password = "123456", Gender = Gender.FEMALE };
            var sut = new AuthBusiness(unitOfWork, mapper, configuration).Register;

            // Act
            await sut(toRegister);

            var foundUser = await unitOfWork.Users.GetByUsernameAsync(toRegister.Username);

            // Assert
            foundUser.Should().NotBeNull();

            // There are 7 test users in database. so a new one must have Id = 8
            foundUser.Id.Should().Be(8);
        }

        [Fact]
        public async void Register_ThrowsAppException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var authRegisterDto = new AuthRegisterDto { Username = "User1", Password="123456"};
            var authBusiness = GetAuthBusiness();

            // Act - Assert
            await authBusiness
                .Awaiting(x => x.Register(authRegisterDto))
                .Should()
                .ThrowAsync<AppException>();
        }

        [Fact]
        public async void Login_ThrownsExceptions_WhenUserNotExists()
        {
            // Arrange
            var authLoginDto = new AuthLoginDto { Username = "not_esisting_username_", Password = "123456" };
            var authBusiness = GetAuthBusiness();

            // Act -Assert
            await authBusiness
                .Awaiting(x => x.Login(authLoginDto))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }

        [Fact]
        public async void Login_ThrownsExceptions_WhenUserIsSoftDeleted()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration = new ConfigurationBuilder().Build();

            var authLoginDto = new AuthLoginDto { Username = "User5", Password = "123456"};
            // Soft Delete User with username User5
            var toSoftDelete = await unitOfWork.Users.GetByUsernameAsync("User5");
            toSoftDelete.DeletedAt = DateTime.UtcNow;

            var authBusiness = new AuthBusiness(unitOfWork, mapper, configuration);

            // Act -Assert
            await authBusiness
                .Awaiting(x => x.Login(authLoginDto))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }

        [Fact]
        public async void Login_ThrownsExceptions_WhenCredentialsAreWrong()
        {
            // Arrange
            // Wrong password.
            var authLoginDto = new AuthLoginDto { Username = "User1", Password = "1234567" };
            var authBusiness = GetAuthBusiness();

            // Act -Assert
            await authBusiness
                .Awaiting(x => x.Login(authLoginDto))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }

        [Fact]
        public async void Login_OnSuccess_RetunsATokenResponseObject()
        {
            // Arrange
            var authLoginDto = new AuthLoginDto { Username = "User3", Password = "123456" };
            var sut = GetAuthBusiness().Login;

            // Act
            var result = await sut(authLoginDto);

            // Assert
            result
                .Should()
                .BeOfType<TokensResponse>();
        }
    }
}

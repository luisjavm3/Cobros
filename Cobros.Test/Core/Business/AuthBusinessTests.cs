using Cobros.API.Core.Business;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Cobros.Test.Core.Business
{
    public class AuthBusinessTests
    {
        [Fact]
        public async void Register_IsSuccessful_WhenUsernameNotExists()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration = new ConfigurationBuilder().Build();

            AuthRegisterDto authRegisterDto = new() { Username = "not_repeated_username", Password = "123456" };

            var sut = new AuthBusiness(unitOfWork, mapper, configuration);

            // Act - Assert
            await sut
                .Awaiting(x => x.Register(authRegisterDto))
                .Should()
                .NotThrowAsync();
        }

        [Fact]
        public async void Register_ThrowsAppException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration= new ConfigurationBuilder().Build();

            var authRegisterDto = new AuthRegisterDto { Username = "User1", Password="123456"};

            var sut = new AuthBusiness(unitOfWork, mapper, configuration);

            // Act - Assert
            await sut
                .Awaiting(x => x.Register(authRegisterDto))
                .Should()
                .ThrowAsync<AppException>();
        }

        [Fact]
        public async void Login_ThrownsExceptions_WhenUserNotExists()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration = new ConfigurationBuilder().Build();
            var authLoginDto = new AuthLoginDto { Username = "not_esisting_username_", Password = "123456" };

            var sut = new AuthBusiness(unitOfWork, mapper, configuration);

            // Act -Assert
            await sut
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

            var sut = new AuthBusiness(unitOfWork, mapper, configuration);

            // Act -Assert
            await sut
                .Awaiting(x => x.Login(authLoginDto))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }

        [Fact]
        public async void Login_ThrownsExceptions_WhenCredentialsAreWrong()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration = new ConfigurationBuilder().Build();
            // Wrong password.
            var authLoginDto = new AuthLoginDto { Username = "User1", Password = "1234567" };

            var sut = new AuthBusiness(unitOfWork, mapper, configuration);

            // Act -Assert
            await sut
                .Awaiting(x => x.Login(authLoginDto))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }

        [Fact]
        public async void Login_OnSuccess_RetunsATokenResponseObject()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetMapper();
            var configuration = helper.GetConfiguration();

            var authLoginDto = new AuthLoginDto { Username = "User3", Password = "123456" };

            var sut = new AuthBusiness(unitOfWork, mapper, configuration);

            // Act
            var result = await sut.Login(authLoginDto);

            // Assert
            result
                .Should()
                .BeOfType<TokensResponse>();
        }
    }
}

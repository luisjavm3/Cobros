using AutoMapper;
using Cobros.API.Core.Business;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Cobros.API.Settings;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text.Json;

namespace Cobros.Test.Core.Business
{
    public class AuthBusinessTests
    {
        private void SetupJwtConfiguration(Mock<IConfiguration> configurationMock)
        {
            var keySection = new Mock<IConfigurationSection>();
            var lifetimeMinutesSection = new Mock<IConfigurationSection>();
            var jwtSection = new Mock<IConfigurationSection>();

            keySection
                .Setup(x => x.Value)
                .Returns("my_long_enough_jwt_key");

            lifetimeMinutesSection
                .Setup(x => x.Value)
                .Returns("30");

            jwtSection
                .Setup(x => x.GetChildren())
                .Returns(new List<IConfigurationSection> { keySection.Object, lifetimeMinutesSection.Object });

            configurationMock
                .Setup(x => x.GetSection(nameof(JwtSettings)))
                .Returns(jwtSection.Object);
        }

        [Fact]
        public async void Register_IsSuccessful_WhenUsernameNotExists()
        {
            // Arrange
            var helper = new TestHelpers();
            var unitOfWork = helper.GetUnitOfWork();
            var mapper = helper.GetAutoMapperInstance();
            var configuration = new Mock<IConfiguration>();
            SetupJwtConfiguration(configuration);

            AuthRegisterDto authRegisterDto = new() { Username = "not_repeated_username", Password = "123456" };

            var sut = new AuthBusiness(unitOfWork, mapper, configuration.Object);

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
            var mapper = helper.GetAutoMapperInstance();
            var configuration = new Mock<IConfiguration>();
            SetupJwtConfiguration(configuration);

            var authRegisterDto = new AuthRegisterDto { Username = "User1", Password="123456"};

            var sut = new AuthBusiness(unitOfWork, mapper, configuration.Object);

            // Act - Assert
            await sut
                .Awaiting(x => x.Register(authRegisterDto))
                .Should()
                .ThrowAsync<AppException>();
        }

        [Fact]
        public async void Login_ThrownsExceptions_WhenUserDoesNotExist()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRespositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var configurationMock = new Mock<IConfiguration>();

            userRespositoryMock
                .Setup(x=>x.GetByUsernameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(null));

            unitOfWorkMock
                .Setup(x => x.Users)
                .Returns(userRespositoryMock.Object);

            SetupJwtConfiguration(configurationMock);

            var sut = new AuthBusiness(unitOfWorkMock.Object, mapperMock.Object, configurationMock.Object);

            // Act -Assert
            await sut
                .Awaiting(x => x.Login(new AuthLoginDto { }))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }
        
        [Fact]
        public async void Login_ThrownsExceptions_WhenUserIsSoftDeleted()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRespositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var configurationMock = new Mock<IConfiguration>();

            // Setting DaletedAt means User is soft deleted
            userRespositoryMock
                .Setup(x=>x.GetByUsernameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(new User { DeletedAt = DateTime.Now.AddMinutes(-1)}));

            unitOfWorkMock
                .Setup(x => x.Users)
                .Returns(userRespositoryMock.Object);

            SetupJwtConfiguration(configurationMock);

            var sut = new AuthBusiness(unitOfWorkMock.Object, mapperMock.Object, configurationMock.Object);

            // Act -Assert
            await sut
                .Awaiting(x => x.Login(new AuthLoginDto { }))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }
        
        [Fact]
        public async void Login_ThrownsExceptions_WhenCredentialsAreWrong()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRespositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var configurationMock = new Mock<IConfiguration>();

            // This hash is an incorrect one.
            userRespositoryMock
                .Setup(x=>x.GetByUsernameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(new User { PasswordHash = "$2a$12$NJFueFwYgUGaE5So851wfeEPnca/JsMEvF0MfwIxrlsozAruJQX5C" }));

            unitOfWorkMock
                .Setup(x => x.Users)
                .Returns(userRespositoryMock.Object);

            SetupJwtConfiguration(configurationMock);

            var sut = new AuthBusiness(unitOfWorkMock.Object, mapperMock.Object, configurationMock.Object);

            // Act -Assert
            await sut
                .Awaiting(x => x.Login(new AuthLoginDto { Password="any"}))
                .Should()
                .ThrowAsync<AppException>()
                .WithMessage("Wrong credentials.");
        }
    }
}

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

namespace Cobros.Test.Core.Business
{
    public class AuthBusinessTests
    {
        //[Fact]
        public async void Register_IsSuccessful_WhenUsernameIsNotRepeated()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userRepository = new Mock<IUserRepository>();
            var mapper = new Mock<IMapper>();
            var configuration = new Mock<IConfiguration>();
            var notRepeatedUsername = "not_repeated_username";

            configuration
                .Setup(x => x.GetSection(It.IsAny<string>()).Get<JwtSettings>())
                .Returns(new JwtSettings { Key = "noooo", LifetimeMinutes = 30});

            userRepository
                .Setup(x => x.GetByUsernameAsync(notRepeatedUsername))
                .Returns(Task.FromResult<User>(null));

            unitOfWork
                .Setup(x => x.Users)
                .Returns(userRepository.Object);

            mapper
                .Setup(x => x.Map<User>(It.IsAny<AuthRegisterDto>()))
                .Returns(new User { Username = notRepeatedUsername, Name = "name", LastName = "lastname", Gender = Gender.FEMALE });

            // System under test - kinda Act
            var sut = new AuthBusiness(unitOfWork.Object, mapper.Object, configuration.Object);

            // Assert
            await sut
                .Awaiting(x => x.Register(new AuthRegisterDto { Username = notRepeatedUsername, Password = "123456", Name = "name", LastName = "lastname", Gender = Gender.FEMALE }))
                .Should()
                .NotThrowAsync();
        }

        //[Fact]
        public async void Register_ThrowsBadRequestException_WhenUsernameIsRepeated()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userRepository = new Mock<IUserRepository>();
            var mapper = new Mock<IMapper>();
            var configuration = new Mock<IConfiguration>();
            var repeatedUsername = "repeated_username";

            userRepository
                .Setup(x => x.GetByUsernameAsync(repeatedUsername))
                .Returns(Task.FromResult<User>(new User { Username = repeatedUsername }));

            unitOfWork
                .Setup(x => x.Users)
                .Returns(() => userRepository.Object);

            // SUT - Kind of Act.
            var sut = new AuthBusiness(unitOfWork.Object, mapper.Object, configuration.Object);

            await sut
                .Awaiting(x => x.Register(new AuthRegisterDto { Username = repeatedUsername }))
                .Should()
                .ThrowAsync<AppException>();
        }
    }
}

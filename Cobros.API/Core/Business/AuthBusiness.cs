using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Helper;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Cobros.API.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Cobros.API.Core.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthBusiness(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        }

        public async Task Register(AuthRegisterDto authRegisterDto)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(authRegisterDto.Username);

            if (existing != null)
                throw new AppException($"Username {authRegisterDto.Username} already exists.");

            var user = _mapper.Map<User>(authRegisterDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(authRegisterDto.Password);

            await _unitOfWork.Users.InsertAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<string> Login(AuthLoginDto authLoginDto)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(authLoginDto.Username);

            // throw an exception if username does not exist.
            if (existing == null)
                throw new AppException("Wrong credentials.");

            // throw an exception if user was deleted.
            if (existing.IsDeleted)
                throw new AppException($"Wrong credentials.");

            // throw an exception if password is incorrect.
            if (!BCrypt.Net.BCrypt.Verify(authLoginDto.Password, existing.PasswordHash))
                throw new AppException("Wrong credentials.");

            try
            {
                _unitOfWork.BeginTransaccion();

                // Remove all Refresh Tokens from a user
                await RefreshTokenHelper.RemoveUserRefreshTokens(existing.Id, _unitOfWork);


            }
            catch (Exception)
            {

                throw;
            }

            return GetToken(existing.Id);
        }

        private string GetToken(int userId)
        {
            var encodedKey = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var signingKey = new SymmetricSecurityKey(encodedKey);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim> {
                new Claim(type: ClaimTypes.NameIdentifier, value: userId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.LifetimeMinutes)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

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
using System.Text;

namespace Cobros.API.Core.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenHelper _refreshTokenHelper;
        private readonly IConfiguration _configuration;

        public AuthBusiness(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            _refreshTokenHelper = new RefreshTokenHelper(_unitOfWork);
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

        public async Task<TokensResponse> Login(AuthLoginDto authLoginDto)
        {
            var existingUser = await _unitOfWork.Users.GetByUsernameAsync(authLoginDto.Username);

            // throw an exception if username does not exist.
            if (existingUser == null)
                throw new AppException("Wrong credentials.");

            // throw an exception if user was deleted.
            if (existingUser.IsDeleted)
                throw new AppException($"Wrong credentials.");

            // throw an exception if password is incorrect.
            if (!BCrypt.Net.BCrypt.Verify(authLoginDto.Password, existingUser.PasswordHash))
                throw new AppException("Wrong credentials.");

            try
            {
                _unitOfWork.BeginTransaccion();

                // Remove all Refresh Tokens from a user
                await _refreshTokenHelper.RemoveUserRefreshTokens(existingUser.Id);
                await _unitOfWork.CompleteAsync();

                var refreshTokenValue = await _refreshTokenHelper.GetUniqueRefreshTokenValue();

                var refeshToken = new RefreshToken
                {
                    User = existingUser,
                    Value = refreshTokenValue,
                    FirstRefreshTokenSession = refreshTokenValue,
                };

                await _unitOfWork.RefreshTokens.InsertAsync(refeshToken);
                await _unitOfWork.CompleteAsync();
                _unitOfWork.Commit();

                return new TokensResponse
                {
                    AccessToken = GetToken(existingUser.Id),
                    RefreshToken = refreshTokenValue,
                };
            }
            catch (Exception er)
            {
                _unitOfWork.Rollback();

                throw new Exception(er.Message);
            }

        }

        public async Task<TokensResponse> RefreshToken(string refreshToken)
        {
            var existingRefreshToken = await _unitOfWork.RefreshTokens.GetByValueAsync(refreshToken);

            if (existingRefreshToken == null)
                throw new AppException($"Refresh token: {refreshToken} does not exist.");

            if(existingRefreshToken.IsExpired)
            {
                existingRefreshToken.ReasonRevoked = "Refresh token attempt when token expired.";
                existingRefreshToken.DeletedAt = DateTime.UtcNow;
                _unitOfWork.RefreshTokens.Update(existingRefreshToken);
                await _unitOfWork.CompleteAsync();

                throw new AppException($"Refresh token: {refreshToken} expired.");
            }

            if(existingRefreshToken.IsRevoked)
            {
                await _refreshTokenHelper.RemoveUserRefreshTokens(existingRefreshToken.User.Id);
                await _unitOfWork.CompleteAsync();
                throw new AppException($"Refresh token: {refreshToken} revoked.");
            }

            // Rotate refresh token.
            try
            {
                _unitOfWork.BeginTransaccion();

                existingRefreshToken.ReasonRevoked = "Refresh token rotated.";

                var newRefrehToken = new RefreshToken
                {
                    User = existingRefreshToken.User,
                    Value = await _refreshTokenHelper.GetUniqueRefreshTokenValue(),
                    PreviousRefeshToken = existingRefreshToken.Value,
                    FirstRefreshTokenSession = existingRefreshToken.FirstRefreshTokenSession,
                };

                _unitOfWork.RefreshTokens.Update(existingRefreshToken);
                await _unitOfWork.RefreshTokens.InsertAsync(newRefrehToken);
                await _unitOfWork.CompleteAsync();
                _unitOfWork.Commit();

                return new TokensResponse
                {
                    AccessToken = GetToken(existingRefreshToken.User.Id),
                    RefreshToken = newRefrehToken.Value
                };
            }
            catch (Exception er)
            {
                _unitOfWork.Rollback();
                throw new Exception(er.Message + er.StackTrace);
            }
        }

        public async Task RevokeRefrehToken(string refreshToken)
        {
            var existing = await _unitOfWork.RefreshTokens.GetByValueAsync(refreshToken);


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

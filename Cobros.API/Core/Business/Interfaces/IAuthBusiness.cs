using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.DTO.User;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface IAuthBusiness
    {
        Task Register(AuthRegisterDto authRegisterDto);
        Task<TokensResponse> Login(AuthLoginDto authLoginDto);
        Task<TokensResponse> RefreshToken(string refreshToken);
        Task RevokeRefreshToken(string refreshToken);
        Task ChangePassword(int userId, ChangePasswordDto changePasswordDto);
    }
}

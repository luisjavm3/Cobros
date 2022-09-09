using Cobros.API.Core.Model.DTO.Auth;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface IAuthBusiness
    {
        Task Register(AuthRegisterDto authRegisterDto);
        Task<string> Login(AuthLoginDto authLoginDto);
    }
}

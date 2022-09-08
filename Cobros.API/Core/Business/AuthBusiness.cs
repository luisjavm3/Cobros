using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Register(AuthRegisterDto authRegisterDto)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(authRegisterDto.Username);

            if (existing == null)
                throw new BadRequestException($"Username {authRegisterDto.Username} already exists.");


            throw new NotImplementedException();
        }
    }
}

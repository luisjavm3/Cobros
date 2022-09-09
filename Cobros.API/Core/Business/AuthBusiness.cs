using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Register(AuthRegisterDto authRegisterDto)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(authRegisterDto.Username);

            if (existing != null)
                throw new BadRequestException($"Username {authRegisterDto.Username} already exists.");

            var user = _mapper.Map<User>(authRegisterDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(authRegisterDto.Password);

            await _unitOfWork.Users.InsertAsync(user);
            await _unitOfWork.CompleteAsync();
        }
    }
}

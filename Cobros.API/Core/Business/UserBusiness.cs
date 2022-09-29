using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Core.Model.Pagination;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserAuthenticatedDto> GetByIdWithCobros(int id)
        {
            var existing = await _unitOfWork.Users.GetByIdWithCobros(id);

            if (existing == null)
                throw new NotFoundException($"User with Id: {id} not found.");

            return _mapper.Map<UserAuthenticatedDto>(existing);
        }

        public async Task<PaginationResult<UserDto>> GetUsers(PaginationParameters paginationParameters)
        {
            var count = await _unitOfWork.Users.CountAsync();
            var users = await _unitOfWork.Users.GetPage(paginationParameters.PageNumber, paginationParameters.PageSize);
            var data = _mapper.Map<IEnumerable<UserDto>>(users);

            return new PaginationResult<UserDto>(data, paginationParameters, count);
        }
    }
}

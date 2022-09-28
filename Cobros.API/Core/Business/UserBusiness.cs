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

        public async Task<UserDto> GetById(int id)
        {
            var existing = await _unitOfWork.Users.GetByIdAsync(id);

            if (existing == null)
                throw new NotFoundException($"User with Id: {id} not found.");

            return _mapper.Map<UserDto>(existing);
        }

        public async Task<PaginationResult<UserDto>> GetRangeOfUsers(PaginationParameters paginationParameters)
        {
            var count = await _unitOfWork.Users.CountAsync();
            var users = await _unitOfWork.Users.GetRangeOfUser(paginationParameters);
            var data = _mapper.Map<IEnumerable<UserDto>>(users);

            return new PaginationResult<UserDto>(data, paginationParameters, count);
        }
    }
}

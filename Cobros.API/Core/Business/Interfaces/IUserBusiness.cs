using Cobros.API.Core.Model;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Entities;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<UserDto> GetById(int id);
        Task<PaginationResult<UserDto>> GetRangeOfUsers(PaginationParameters paginationParameters);
    }
}

using Cobros.API.Core.Model;
using Cobros.API.Core.Model.DTO.User;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<UserDto> GetById(int id);
        Task<IEnumerable<UserDto>> GetRangeOfUsers(PaginationParameters paginationParameters);
    }
}

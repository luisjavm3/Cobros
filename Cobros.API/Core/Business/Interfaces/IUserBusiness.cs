using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Core.Model.Pagination;
using Cobros.API.Entities;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<UserAuthenticatedDto> GetByIdWithCobros(int id);
        Task<PaginationResult<UserDto>> GetUsers(PaginationParameters paginationParameters);
    }
}

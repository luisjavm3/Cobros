using Cobros.API.Core.Model.DTO.Cobro;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface ICobroBusiness
    {
        Task<CobroDto> GetCobroById(int id);
    }
}

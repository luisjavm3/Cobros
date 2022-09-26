using Cobros.API.Core.Model.DTO.Cobro;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface ICobroBusiness
    {
        Task<CobroDto> GetCobroById(int id);
        Task UpdateCobro(int id, CobroUpdateDto cobroUpdateDto);
        Task CreateCobro(CobroCreateDto cobroCreateDto);
        Task DeleteCobro(int id);
        Task<IEnumerable<CobroDto>> GetAllCobros();
    }
}

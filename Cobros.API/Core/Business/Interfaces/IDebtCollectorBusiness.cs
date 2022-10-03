using Cobros.API.Core.Model.DTO.DebtCollector;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface IDebtCollectorBusiness
    {
        Task InsertDebtCollector(DebtCollectorCreateDto debtCollectorCreateDto);
    }
}

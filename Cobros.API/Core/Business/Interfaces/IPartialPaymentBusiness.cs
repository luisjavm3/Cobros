using Cobros.API.Core.Model.DTO.PartialPayment;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface IPartialPaymentBusiness
    {
        Task InsertPartialPayment(int loanId, PartialPaymentCreateDto partialPaymentCreateDto);
    }
}

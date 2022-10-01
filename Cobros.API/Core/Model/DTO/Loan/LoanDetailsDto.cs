using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Core.Model.DTO.Customer;
using Cobros.API.Core.Model.DTO.PartialPayment;

namespace Cobros.API.Core.Model.DTO.Loan
{
    public class LoanDetailsDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int LoanInterest { get; set; }
        public int RoutePosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int Balance { get; set; }
        public int TotalPaid { get; set; }
        public int Total { get; set; }

        public CobroSimpleDto Cobro { get; set; }
        public CustomerDto Customer { get; set; }
        public IEnumerable<PartialPaymentDto> PartialPayments { get; set; }
    }
}

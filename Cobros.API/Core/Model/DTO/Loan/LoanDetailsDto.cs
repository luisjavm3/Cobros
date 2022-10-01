using Cobros.API.Entities;

namespace Cobros.API.Core.Model.DTO.Loan
{
    public class LoanDetailsDto
    {
        public int Value { get; set; }
        public int LoanInterest { get; set; }
        public int RoutePosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public int CobroId { get; set; }
        //public Cobros.API.Entities.Cobro Cobro { get; set; }

        //public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Balance { get; set; }
        public int TotalPaid { get; set; }

        public IEnumerable<Cobros.API.Entities.PartialPayment> PartialPayments { get; set; }
    }
}

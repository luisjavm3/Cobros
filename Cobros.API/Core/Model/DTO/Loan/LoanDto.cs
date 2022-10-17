using Cobros.API.Core.Model.DTO.Customer;
using Cobros.API.Entities;

namespace Cobros.API.Core.Model.DTO.Loan
{
    public class LoanDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CobroId { get; set; }
        public CustomerSimpleDto Customer { get; set; }
        public int Value { get; set; }
        public int LoanInterest { get; set; }
        public int RoutePosition { get; set; }
        public int Balance { get; set; }
        public double Total { get; set; }
        public int TotalPaid { get; set; }
    }
}

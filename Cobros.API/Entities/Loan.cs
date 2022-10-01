namespace Cobros.API.Entities
{
    public class Loan:Entity
    {
        public int Value { get; set; }
        public int LoanInterest { get; set; }
        public int RoutePosition { get; set; }

        public int CobroId { get; set; }
        public virtual Cobro Cobro { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int Balance { get; set; }

        public virtual IEnumerable<PartialPayment> PartialPayments { get; set; }
    }
}

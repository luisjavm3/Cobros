namespace Cobros.API.Entities
{
    public class PartialPayment:Entity
    {
        public int Value { get; set; }

        public int LoanId { get; set; }
        public virtual Loan Loan { get; set; }
    }
}

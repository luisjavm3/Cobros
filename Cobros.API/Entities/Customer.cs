namespace Cobros.API.Entities
{
    public class Customer:Person
    {
        public virtual IEnumerable<Loan> Loans { get; set; }
    }
}

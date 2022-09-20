namespace Cobros.API.Entities
{
    public class Customer:Person
    {
        public IEnumerable<Loan> Loans { get; set; }
    }
}

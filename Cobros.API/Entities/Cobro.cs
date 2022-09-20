namespace Cobros.API.Entities
{
    public class Cobro : Entity
    {
        public string Name { get; set; }
        public User User { get; set; }
        public DebtCollector DebtCollector { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}

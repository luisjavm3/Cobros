namespace Cobros.API.Entities
{
    public class DebtCollector:Person
    {
        //public int CobroId { get; set; }
        public Cobro Cobro { get; set; }
    }
}

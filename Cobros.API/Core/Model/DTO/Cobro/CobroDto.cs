namespace Cobros.API.Core.Model.DTO.Cobro
{
    public class CobroDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int DebtCollectorId { get; set; }
        public int Loans { get; set; }
        public int Balance { get; set; }
    }
}

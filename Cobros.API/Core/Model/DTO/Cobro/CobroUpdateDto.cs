using Cobros.API.Entities;

namespace Cobros.API.Core.Model.DTO.Cobro
{
    public class CobroUpdateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int DebtCollectorId { get; set; }
    }
}

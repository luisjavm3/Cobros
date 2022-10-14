using Cobros.API.Core.Model.DTO.DebtCollector;
using Cobros.API.Core.Model.DTO.User;

namespace Cobros.API.Core.Model.DTO.Cobro
{
    public class CobroDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserSimpleDto User { get; set; }
        public DebtCollectorSimpleDto DebtCollector { get; set; }
        public int Loans { get; set; }
        public int Balance { get; set; }
    }
}

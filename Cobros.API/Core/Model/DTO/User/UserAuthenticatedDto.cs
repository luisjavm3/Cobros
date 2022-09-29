
using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Entities;

namespace Cobros.API.Core.Model.DTO.User
{
    public class UserAuthenticatedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public IEnumerable<CobroDto> Cobros { get; set; }
    }
}

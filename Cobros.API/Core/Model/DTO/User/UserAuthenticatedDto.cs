
using Cobros.API.Entities;

namespace Cobros.API.Core.Model.DTO.User
{
    public class UserAuthenticatedDto
    {
        public string Username { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Cobros.API.Entities.Cobro> Cobros { get; set; }
    }
}

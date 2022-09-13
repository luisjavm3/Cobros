using Cobros.API.Entities;

namespace Cobros.API.Core.Model.DTO.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
    }
}

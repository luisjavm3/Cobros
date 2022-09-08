namespace Cobros.API.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; } = Role.USER;
        public string PasswordHash { get; set; }
    }
}

namespace Cobros.API.Entities
{
    public class User : Person
    {
        public string Username { get; set; }
        public Role Role { get; set; } = Role.USER;
        public string PasswordHash { get; set; }
        public IEnumerable<Cobro> Cobros { get; set; }
    }
}

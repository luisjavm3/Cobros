namespace Cobros.API.Entities
{
    public class RefreshToken : Entity
    {
        public string Value { get; set; }
        public string FirstRefreshTokenSession { get; set; }
        public string PreviousRefeshToken { get; set; }
        public string ReasonRevoked { get; set; }
        public bool IsRevoked => ReasonRevoked != null ? true : false;
        public bool IsExpired => CreatedAt.AddDays(1) <= DateTime.UtcNow;
        public User User { get; set; }
    }
}

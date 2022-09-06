namespace Cobros.API.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeleteAt { get; init; }
        public bool IsDeleted => DeleteAt != null ? true : false;
    }
}

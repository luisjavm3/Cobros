﻿namespace Cobros.API.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted => DeletedAt != null ? true : false;
    }
}

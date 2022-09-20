namespace Cobros.API.Entities
{
    public class PhoneNumber:Entity
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public Person Person { get; set; }
    }
}

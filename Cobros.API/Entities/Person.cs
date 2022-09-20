namespace Cobros.API.Entities
{
    public abstract class Person : Entity
    {
        public string NationalID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
    }
}

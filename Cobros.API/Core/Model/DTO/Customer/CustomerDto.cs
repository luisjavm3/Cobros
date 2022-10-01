using Cobros.API.Entities;

namespace Cobros.API.Core.Model.DTO.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string NationalID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
    }
}

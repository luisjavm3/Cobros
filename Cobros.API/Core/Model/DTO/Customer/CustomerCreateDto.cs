using Cobros.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Customer
{
    public class CustomerCreateDto
    {
        [Required(ErrorMessage ="NationalID is required.")]
        [MinLength(7)]
        public string NationalID { get; set; }

        [Required(ErrorMessage ="Name is required.")]
        [MinLength(2)]
        public string Name { get; set; }

        [Required(ErrorMessage ="LastName is required.")]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage ="Address is required.")]
        [MinLength(4)]
        public string Address { get; set; }
    }
}

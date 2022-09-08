using Cobros.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Auth
{
    public class AuthRegisterDto
    {
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}

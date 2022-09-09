using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Auth
{
    public class AuthLoginDto
    {
        [Required(ErrorMessage ="Username is required.")]
        [MaxLength(255, ErrorMessage ="Username must have less than or equal to 255 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        [MaxLength(255, ErrorMessage ="Password must have less than or equal to 255 characters.")]
        public string Password { get; set; }
    }
}

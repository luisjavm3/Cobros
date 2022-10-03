using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.User
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "CurrentPassword isRequired.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage ="NewPassword isRequired.")]
        [MinLength(6, ErrorMessage = "NewPassword must have at least 6 characters.")]
        public string NewPassword { get; set; }

    }
}

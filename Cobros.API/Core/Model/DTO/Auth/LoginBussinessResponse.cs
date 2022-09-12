using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Auth
{
    public class LoginBussinessResponse
    {
        [Required]
        public string accessToken { get; set; }

        [Required]
        public string refreshToken { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Auth
{
    public class TokensResponse
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}

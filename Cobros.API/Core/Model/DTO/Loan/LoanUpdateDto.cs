using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Loan
{
    public class LoanUpdateDto
    {
        [Required(ErrorMessage ="RoutePosition is required.")]
        [Range(1, int.MaxValue, ErrorMessage ="RoutePosition must be greater than 0.")]
        public int RoutePosition { get; set; }
    }
}

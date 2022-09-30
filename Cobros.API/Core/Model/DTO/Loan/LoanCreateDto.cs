using Cobros.API.Core.Model.Validation;
using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Loan
{
    public class LoanCreateDto
    {
        [Required(ErrorMessage ="Name is required.")]
        [DivisibleBy10]
        public int Value { get; set; }

        [Required(ErrorMessage ="LoanInterest is required.")]
        [Range(1,int.MaxValue, ErrorMessage = "LoanInterest must be greater than 0.")]
        public int LoanInterest { get; set; }

        [Required(ErrorMessage ="RoutePosition is required.")]
        public int RoutePosition { get; set; }

        [Required(ErrorMessage ="CustomerId is required.")]
        public int CustomerId { get; set; }
    }
}

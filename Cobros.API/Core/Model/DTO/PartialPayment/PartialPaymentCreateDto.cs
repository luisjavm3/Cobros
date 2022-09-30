using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.PartialPayment
{
    public class PartialPaymentCreateDto
    {
        [Required(ErrorMessage ="Value is required.")]
        [Range(1, int.MaxValue, ErrorMessage ="Value must be greater than 0.")]
        public int Value { get; set; }
    }
}

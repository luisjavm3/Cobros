using Cobros.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Cobro
{
    public class CobroUpdateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(4 ,ErrorMessage ="Name must have at least 4 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "DebtCollectorId is required.")]
        public int DebtCollectorId { get; set; }
    }
}

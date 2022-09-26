using Cobros.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.DTO.Cobro
{
    public class CobroCreateDto
    {
        [Required(ErrorMessage ="Name is required.")]
        [MinLength(4, ErrorMessage ="Name must have at least 4 characters.")]
        public string Name { get; set; }
        public int? UserId { get; set; }
        public int? DebtCollectorId { get; set; }
    }
}

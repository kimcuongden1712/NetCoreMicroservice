using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product
{
    public class CreateProductDTO : CreateOrUpdateProductDTO
    {
        [Required]
        public string No { get; set; }
    }
}
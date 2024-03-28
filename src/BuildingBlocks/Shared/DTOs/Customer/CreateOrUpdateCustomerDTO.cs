using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs.Customer
{
    public abstract class CreateOrUpdateCustomerDTO
    {
        [Required]
        [MaxLength(250, ErrorMessage = "Maximum length for Product Name is 250 characters")]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for Product Name is 100 characters")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for Product Name is 100 characters")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }
    }
}
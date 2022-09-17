using Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.API.Entities
{
    public class Customer : EntityAuditBase<int>
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

using Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Entites
{
    public class CatalogCustomer : EntityAuditBase<long>
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
        public string EmailAddress { get; set; }
    }
}

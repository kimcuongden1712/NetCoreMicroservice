using Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.API.Entities
{
    public class Brand : EntityAuditBase<long>
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string No { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Summary { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public IList<CatalogProduct> Items { get; private set; } = new List<CatalogProduct>();
    }
}

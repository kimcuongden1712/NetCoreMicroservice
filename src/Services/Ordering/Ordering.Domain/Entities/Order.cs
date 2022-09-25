﻿using Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities
{
    public class Order : EntityAuditBase<long>
    {
        [Required]
        public string UserName { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string EmailAddress { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string InvoiceAddress { get; set; }
    }
}
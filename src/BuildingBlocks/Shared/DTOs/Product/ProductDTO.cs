﻿namespace Shared.DTOs.Product
{
    public class ProductDTO
    {
        public long Id { get; set; }

        public string No { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
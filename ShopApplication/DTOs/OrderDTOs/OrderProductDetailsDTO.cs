using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.OrderDTOs
{
    public class OrderProductDetailsDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}

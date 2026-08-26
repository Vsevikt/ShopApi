using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.OrderDTOs
{
    public class OrderMessageDTO
    {
        public Guid? UserId { get; set; }
        public List<OrderProductDetailsDTO> Products { get; set; } = new();
        public decimal TotalPrice { get; set; }
        public string? UserEmail { get; set; }
    }
}

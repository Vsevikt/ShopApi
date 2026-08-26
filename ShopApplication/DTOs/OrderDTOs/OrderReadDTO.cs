using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.OrderDTOs
{
    public class OrderReadDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public bool Paid { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new();
    }
}

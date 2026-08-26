using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.OrderDTOs
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}

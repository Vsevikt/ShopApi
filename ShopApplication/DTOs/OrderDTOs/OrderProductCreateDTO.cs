using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.OrderDTOs
{
    public class OrderProductCreateDTO
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}

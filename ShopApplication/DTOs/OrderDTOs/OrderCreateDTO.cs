using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopApplication.DTOs.OrderDTOs
{
    public class OrderCreateDTO
    {
        public List<OrderProductCreateDTO> Products { get; set; } = new();
    }
}

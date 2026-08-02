using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.CartDTOs
{
    public class CartCreateDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}

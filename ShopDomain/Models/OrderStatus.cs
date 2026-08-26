using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDomain.Models
{
    public enum OrderStatus
    {
        New,
        Processing,
        Shipped,
        Completed,
        Cancelled
    }
}

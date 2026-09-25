using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.UserDTOs
{
    public class DeliveryAddressReadDTO
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
        public string PostalCode { get; set; } = string.Empty;
    }
}
    
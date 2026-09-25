namespace ShopApplication.DTOs.DeliveryAddress;

public class DeliveryAddressCreateDTO
{
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string House { get; set; } = string.Empty;
    public string? Apartment { get; set; }
    public string PostalCode { get; set; } = string.Empty;
}
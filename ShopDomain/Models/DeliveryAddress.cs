using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDomain.Models;

[Table("delivery_addresses")]
public class DeliveryAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("city")]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("street")]
    public string Street { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Column("house")]
    public string House { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("apartment")]
    public string? Apartment { get; set; }

    [MaxLength(20)]
    [Column("postal_code")]
    public string PostalCode { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}
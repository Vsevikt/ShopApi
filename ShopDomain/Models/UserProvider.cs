using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDomain.Models
{
    [Table("users_providers")]
    public class UserProvider
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("provider_id")]
        public int ProviderId { get; set; }

        [Column("number_provider")]
        public string NumberProvider { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Provider Provider { get; set; } = null!;
    }
}
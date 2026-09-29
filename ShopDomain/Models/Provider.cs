using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDomain.Models
{
    [Table("providers")]
    public class Provider
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<UserProvider> UserProviders { get; set; } = new List<UserProvider>();
    }
}
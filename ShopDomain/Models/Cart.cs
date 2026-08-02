using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopDomain.Models
{
    [Table("carts")]
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        //[Column("user_id")]
        //public Guid UserId { get; set; }
 
        //public User User { get; set; } = null!;

        [Column("product_id")]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopDomain.Models
{
    [Table("orders_detail")]
    public class OrderDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("count")]
        public int Count { get; set; }
    }
}

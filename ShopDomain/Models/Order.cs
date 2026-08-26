using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopDomain.Models
{
    [Table("orders")]
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("status")]
        public OrderStatus Status { get; set; } = OrderStatus.New;

        [Column("paid")]
        public bool Paid { get; set; } = false;

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

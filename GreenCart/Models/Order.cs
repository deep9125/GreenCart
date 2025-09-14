using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCart.Models
{
    public enum OrderStatus { Pending, Shipped, Delivered, Canceled }
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingAddress { get; set; }

        // Foreign Key for the buyer
        public int BuyerId { get; set; }

        // Navigation properties
        public virtual ApplicationUser Buyer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

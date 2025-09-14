using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCart.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // Simplified from a separate model

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int StockQuantity { get; set; }

        // Foreign Key for the seller
        public int SellerId { get; set; }

        // Navigation properties
        public virtual ApplicationUser Seller { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

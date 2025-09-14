using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCart.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // Foreign Keys for relationships
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}

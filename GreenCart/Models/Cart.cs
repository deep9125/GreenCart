using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenCart.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenCart.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } // We will NEVER store plain text passwords

        [Required]
        public string Role { get; set; } // "Seller" or "Buyer"

        // Navigation properties from the 4-model design
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

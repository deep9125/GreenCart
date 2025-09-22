using System.ComponentModel.DataAnnotations;

namespace GreenCart.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(0.01, 100000.00, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        [Url]
        public string? ImageUrl { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be 0 or greater.")]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }
    }
}

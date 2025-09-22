using System.Collections.Generic;
using GreenCart.Models;

namespace GreenCart.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<OrderItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}

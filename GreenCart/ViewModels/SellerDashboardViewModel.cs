using System.Collections.Generic;
using GreenCart.Models;

namespace GreenCart.ViewModels
{
    public class SellerDashboardViewModel
    {
        public IEnumerable<Product> MyProducts { get; set; }
        public IEnumerable<Order> IncomingOrders { get; set; }
    }
}

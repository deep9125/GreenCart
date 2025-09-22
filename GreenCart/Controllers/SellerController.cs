using System.Linq;
using GreenCart.Models;
using GreenCart.Repository;
using GreenCart.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenCart.Controllers
{
    public class SellerController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        public SellerController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }
        public IActionResult Dashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userId == null || userRole != "Seller")
            {
                return RedirectToAction("Login", "Account");
            }
            var sellerProducts = _productRepository.GetBySellerId(userId.Value);
            var incomingOrders = _orderRepository.GetOrdersBySellerId(userId.Value);
            var viewModel = new SellerDashboardViewModel
            {
                MyProducts = sellerProducts,
                IncomingOrders = incomingOrders
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || HttpContext.Session.GetString("UserRole") != "Seller")
            {
                return RedirectToAction("Login", "Account");
            }
            var order = _orderRepository.GetById(orderId);
            if (order != null && order.OrderItems.Any(oi => oi.Product.SellerId == userId.Value))
            {
                order.Status = status;
                _orderRepository.Update(order);
            }
            return RedirectToAction("Dashboard");
        }
    }
}

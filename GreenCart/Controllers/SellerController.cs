using System.Linq;
using GreenCart.Models;
using GreenCart.Repository;
using GreenCart.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenCart.Controllers
{
    public class SellerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public SellerController(AppDbContext context,IProductRepository productRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
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
        public IActionResult UpdateOrderStatus(int orderItemId, OrderStatus status)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || HttpContext.Session.GetString("UserRole") != "Seller")
            {
                return RedirectToAction("Login", "Account");
            }
            var orderItem = _orderItemRepository.GetById(orderItemId);
            if (orderItem != null && orderItem.Product.SellerId == userId.Value)
            {
                orderItem.Status = status;
                _orderItemRepository.Update(orderItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}

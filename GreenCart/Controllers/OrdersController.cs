using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using GreenCart.Models;
using GreenCart.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenCart.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }
        public IActionResult History()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var orders = _orderRepository.GetOrdersByBuyerId(userId.Value);
            return View(orders);
        }
        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(string shippingAddress)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var cart = _cartRepository.GetByUserId(userId.Value);
            if (cart == null || !cart.Items.Any())
            {
                return RedirectToAction("Index", "Products");
            }
            var order = new Order
            {
                BuyerId = userId.Value,
                OrderDate = DateTime.Now,
                ShippingAddress = shippingAddress,
                Status = OrderStatus.Pending,
                OrderItems = cart.Items.Select(cartItem => new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price 
                }).ToList()
            };
            order.TotalAmount = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);
            _orderRepository.Add(order);
            _cartRepository.ClearCart(userId.Value);
            return RedirectToAction("History");
        }
    }
}

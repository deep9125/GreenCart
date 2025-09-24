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
        private readonly IProductRepository _productRepository;
        public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
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
            if (cart == null || !cart.Items.Any()) return RedirectToAction("Index", "Products");

            var order = new Order
            {
                BuyerId = userId.Value,
                OrderDate = DateTime.Now,
                ShippingAddress = shippingAddress,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;
            foreach (var cartItem in cart.Items)
            {
                var product = _productRepository.GetById(cartItem.ProductId);
                if (product == null || product.StockQuantity < cartItem.Quantity)
                {
                    TempData["ErrorMessage"] = $"Sorry, there are only {product?.StockQuantity ?? 0} units of '{product?.Name}' available.";
                    return RedirectToAction("Index", "Cart");
                }
                product.StockQuantity -= cartItem.Quantity;
                _productRepository.Update(product);
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    Status = OrderStatus.Pending,
                    Price = product.Price
                };
                order.OrderItems.Add(orderItem);
                total += (product.Price * cartItem.Quantity);
            }
            order.TotalAmount = total;
            _orderRepository.Add(order);
            _cartRepository.ClearCart(userId.Value);
            return RedirectToAction("History");
        }
    }
}

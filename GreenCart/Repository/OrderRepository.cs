using System.Collections.Generic;
using GreenCart.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GreenCart.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByBuyerId(int buyerId)
        {
            return _context.Orders
                .Where(o => o.BuyerId == buyerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
    }
}

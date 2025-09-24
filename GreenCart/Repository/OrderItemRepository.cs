using System.Collections.Generic;
using GreenCart.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GreenCart.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public OrderItem? GetById(int id)
        {
            return _context.OrderItems
                .Include(oi => oi.Product)
                .FirstOrDefault(oi => oi.Id == id);
        }

        public IEnumerable<OrderItem> GetByOrderId(int orderId)
        {
            return _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Product) 
                .ToList();
        }
        public void Update(OrderItem item)
        {
            _context.OrderItems.Update(item);
            _context.SaveChanges();
        }
    }
}

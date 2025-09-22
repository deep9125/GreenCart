using System.Collections.Generic;
using GreenCart.Models;

namespace GreenCart.Repository
{
    public interface IOrderRepository
    {
        void Add(Order order);
        IEnumerable<Order> GetOrdersByBuyerId(int buyerId);
        IEnumerable<Order> GetOrdersBySellerId(int sellerId);
        Order? GetById(int id);      
        void Update(Order order);
    }
}

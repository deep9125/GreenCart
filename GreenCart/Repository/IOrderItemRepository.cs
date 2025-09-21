using System.Collections.Generic;
using GreenCart.Models;

namespace GreenCart.Repository
{
    public interface IOrderItemRepository
    {
        OrderItem? GetById(int id);
        IEnumerable<OrderItem> GetByOrderId(int orderId);
    }
}

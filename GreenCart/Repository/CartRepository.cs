using System.Linq;
using GreenCart.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenCart.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public Cart GetByUserId(int userId)
        {
            var cart = _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            return cart;
        }
        public void AddItem(int userId, int productId)
        {
            var cart = GetByUserId(userId);
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem { ProductId = productId, Quantity = 1 };
                cart.Items.Add(cartItem);
            }
            else
            { 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }
        public void RemoveItem(int cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }
        public void ClearCart(int userId)
        {
            var cart = _context.Carts.Include(c => c.Items).FirstOrDefault(c => c.UserId == userId);
            if (cart != null && cart.Items.Any())
            {
                _context.CartItems.RemoveRange(cart.Items);
                _context.SaveChanges();
            }
        }
    }
}

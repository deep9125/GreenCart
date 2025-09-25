using System.Linq;
using GreenCart.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenCart.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartController(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var cart = _cartRepository.GetByUserId(userId.Value);
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            if (quantity < 1)
            {
                quantity = 1;
            }
            var product = _productRepository.GetById(productId);
            if (product == null)
            {
                return NotFound();
            }
            var cart = _cartRepository.GetByUserId(userId.Value);
            var existingCartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            int quantityAlreadyInCart = existingCartItem?.Quantity ?? 0;
            if ((quantityAlreadyInCart + quantity) > product.StockQuantity)
            {
                TempData["ErrorMessage"] = $"You can't add {quantity} unit(s) of '{product.Name}'. Only {product.StockQuantity - quantityAlreadyInCart} more are available.";
                return RedirectToAction("index", "Products");
            }
            _cartRepository.AddItem(userId.Value, productId, quantity);
            return RedirectToAction("Index", "Products");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            _cartRepository.RemoveItem(cartItemId);
            return RedirectToAction("Index");
        }
    }
}

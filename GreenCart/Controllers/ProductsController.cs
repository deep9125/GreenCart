using System.Linq;
using AutoMapper;
using GreenCart.Models;
using GreenCart.Repository;
using GreenCart.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenCart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository,ICartRepository cartRepository ,IMapper mapper)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            var userId = HttpContext.Session.GetInt32("UserId");
            Cart userCart = null;
            if (userId != null)
            {
                userCart = _cartRepository.GetByUserId(userId.Value);
            }
            var viewModel = products.Select(p => new MarketplaceProductViewModel
            {
                Product = p,
                QuantityInCart = userCart?.Items.FirstOrDefault(i => i.ProductId == p.Id)?.Quantity ?? 0
            }).ToList();
            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            var userId = HttpContext.Session.GetInt32("UserId");
            Cart userCart = null;
            if (userId != null)
            {
                userCart = _cartRepository.GetByUserId(userId.Value);
            }
            var viewModel = new MarketplaceProductViewModel 
            {
                Product= product,
                QuantityInCart = userCart?.Items.FirstOrDefault(i => i.ProductId == product.Id)?.Quantity ?? 0
            };
            return View(viewModel);
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Seller")
            {
                return RedirectToAction("Login", "Account");
            }
            return View(new ProductFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductFormViewModel viewModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (HttpContext.Session.GetString("UserRole") != "Seller" || userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(viewModel);
                product.SellerId = userId.Value;
                _productRepository.Add(product);
                return RedirectToAction(nameof(Details) ,new { id = product.Id });
            }
            return View(viewModel);
        }
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (HttpContext.Session.GetString("UserRole") != "Seller" || userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            if (product.SellerId != userId.Value)
            {
                return Forbid(); 
            }
            var viewModel = _mapper.Map<ProductFormViewModel>(product);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductFormViewModel viewModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (HttpContext.Session.GetString("UserRole") != "Seller" || userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id != viewModel.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var product = _productRepository.GetById(id);
                if (product == null) return NotFound();
                if (product.SellerId != userId.Value)
                {
                    return Forbid();
                }
                _mapper.Map(viewModel, product);
                _productRepository.Update(product);
                return RedirectToAction(nameof(Details), new { id = product.Id });
            }
            return View(viewModel);
        }
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (HttpContext.Session.GetString("UserRole") != "Seller" || userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            if (product.SellerId != userId.Value)
            {
                return Forbid();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (HttpContext.Session.GetString("UserRole") != "Seller" || userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            if (product.SellerId != userId.Value)
            {
                return Forbid();
            }
            _productRepository.Delete(id);
            return RedirectToAction("Dashboard", "Seller");
        }
    }
}

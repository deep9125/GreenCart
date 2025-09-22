using Microsoft.AspNetCore.Mvc;
using GreenCart.Repository;
using GreenCart.Models;
using GreenCart.ViewModels;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace GreenCart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            return View(product);
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using GreenCart.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenCart.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(p => p.Seller).ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.Include(p => p.Seller).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public IEnumerable<Product> GetBySellerId(int sellerId)
        {
            return _context.Products
                .Where(p => p.SellerId == sellerId)
                .Include(p => p.Seller)
                .ToList();
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}

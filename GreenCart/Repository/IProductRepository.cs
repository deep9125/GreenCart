using System.Collections.Generic;
using GreenCart.Models;

namespace GreenCart.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        IEnumerable<Product> GetBySellerId(int sellerId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}

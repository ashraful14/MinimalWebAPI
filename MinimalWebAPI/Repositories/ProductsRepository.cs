
using Microsoft.EntityFrameworkCore;
using MinimalWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalWebAPI.Repositories
{
    public class ProductsRepository : IProductRepository
    {
        protected ProductDbContext _context;
        public ProductsRepository(ProductDbContext context)
        {
            _context = context;
        }
        public Product GetProduct(int id)
        {
            var data= _context.Products.FirstOrDefault(p => p.ProdId == id);
            return data;
        }
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProduct(int Id)
        {
            var product = _context.Products.FirstOrDefault(p=> p.ProdId==Id);
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}

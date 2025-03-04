using System.Collections.Generic;
using System.Linq;
using websitecafe.Models;
using Microsoft.EntityFrameworkCore;

namespace websitecafe.DAO
{
    public class ProductDao
    {
        private readonly DBWebcafeContext _context;

        public ProductDao()
        {
            _context = new DBWebcafeContext();
        }

        public List<Product> GetAllProducts(int pageNumber, int pageSize)
        {
            return _context.Products
                           .Include("Category")
                           .OrderBy(p => p.Id) // Sắp xếp để đảm bảo thứ tự
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public List<Product> GetProductsByPage(int pageNumber, int pageSize, out int totalProducts)
        {
            totalProducts = _context.Products.Count();

            return _context.Products
                           .OrderBy(p => p.Id)
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public int GetTotalProductsCount()
        {
            return _context.Products.Count();
        }

        public List<Product> GetTopViewedProducts(int top = 8)
        {
            return _context.Products
                           .OrderByDescending(p => p.View)
                           .Take(top)
                           .ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include("Category").FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.CategoryId = product.CategoryId;
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}

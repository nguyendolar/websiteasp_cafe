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
                           .Include("Category") // Sử dụng nameof tránh lỗi refactoring
                           .OrderBy(p => p.Id)
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public List<Product> GetProductsByPage(int pageNumber, int pageSize, out int totalProducts, int? categoryId, string search)
        {
            var query = _context.Products.Include("Category").AsQueryable();

            // Lọc theo danh mục (nếu có)
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            // Tìm kiếm theo tên sản phẩm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            totalProducts = query.Count();

            return query.OrderBy(p => p.Id)
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
                           .Include("Category")
                           .OrderByDescending(p => p.View)
                           .Take(top)
                           .ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                           .Include("Category")
                           .FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public bool UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null)
                return false;

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.CategoryId = product.CategoryId;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}

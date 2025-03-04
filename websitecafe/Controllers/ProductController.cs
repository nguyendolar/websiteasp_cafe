using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitecafe.DAO;

namespace websitecafe.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDao _productDao;

        public ProductController()
        {
            _productDao = new ProductDao();
        }

        public ActionResult Index(int page = 1, int pageSize = 6, int? categoryId = null, string search = "")
        {
            int totalProducts;
            var products = _productDao.GetProductsByPage(page, pageSize, out totalProducts, categoryId, search);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Categories = _productDao.GetAllCategories(); // Lấy danh sách danh mục

            return View(products);
        }
    }
}
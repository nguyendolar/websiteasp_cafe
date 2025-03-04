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
        public ActionResult Index(int page = 1)
        {
            int pageSize = 6; // Số sản phẩm trên mỗi trang
            int totalProducts;

            ProductDao productDao = new ProductDao();
            var products = productDao.GetProductsByPage(page, pageSize, out totalProducts);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            return View(products);
        }
    }
}
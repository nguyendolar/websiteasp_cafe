using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using websitecafe.DAO;
using websitecafe.Models;

namespace websitecafe.Controllers.Admin
{
    public class AdminProductController : Controller
    {
        ProductDao productDao = new ProductDao();
        CategoryDao categoryDao = new CategoryDao();

        // GET: AdminProduct
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = productDao.GetAllProducts();
            ViewBag.listCategory = categoryDao.GetAllCategories();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Product product)
        {
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                product.ImageUrl = fileName;
            }

            productDao.AddProduct(product);
            return RedirectToAction("Index", new { msg = "1" });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Product product)
        {
            var existingProduct = productDao.GetProductById(product.Id);
            if (existingProduct == null)
            {
                return RedirectToAction("Index", new { msg = "3" }); // Không tìm thấy sản phẩm
            }

            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                product.ImageUrl = fileName;
            }
            else
            {
                product.ImageUrl = existingProduct.ImageUrl;
            }

            productDao.UpdateProduct(product);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(int id)
        {
            var product = productDao.GetProductById(id);
            if (product != null)
            {
                productDao.DeleteProduct(id);
                return RedirectToAction("Index", new { msg = "1" });
            }
            return RedirectToAction("Index", new { msg = "2" }); // Không tìm thấy sản phẩm
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitecafe.DAO;

namespace websitecafe.Controllers
{
    public class HomeController : Controller
    {
        ProductDao _productDao = new ProductDao();


        public ActionResult Index()
        {
            ViewBag.Products = _productDao.GetTopViewedProducts(8);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
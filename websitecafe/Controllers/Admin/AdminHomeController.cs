using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitecafe.Models;

namespace websitecafe.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHome
        public ActionResult Index()
        {
            // Kiểm tra xem ADMIN đã đăng nhập chưa
            User user = Session["ADMIN"] as User;
            if (user == null)
            {
                return RedirectToAction("Login", "AdminAuthen");
            }

            return View();
        }
    }
}
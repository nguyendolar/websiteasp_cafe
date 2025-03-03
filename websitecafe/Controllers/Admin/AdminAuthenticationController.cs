using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Collections.Specialized.BitVector32;
using System.Web.Mvc;
using websitecafe.Models;
using websitecafe.Admin.DAO;

namespace websitecafe.Controllers.Admin
{
	public class AdminAuthenticationController : Controller
    {
        UserDao userDao = new UserDao();
        // GET: AdminAuthentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            User user = new User()
            {
                Email = form["userName"],
                Password = form["password"]
            };
            bool checkLogin = userDao.checkLogin(user.Email, user.Password);
            if (checkLogin)
            {
                var userInformation = userDao.getUserByEmail(user.Email);

                if (userInformation.IsAdmin == false)
                {
                    ViewBag.mess = "Bạn không có quyền truy cập vào trang quản trị";
                    return View("Login");
                }
                else
                {
                    Session.Add("ADMIN", userInformation);
                    return RedirectToAction("Index", "AdminHome");
                }

            }
            else
            {
                ViewBag.mess = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                return View("Login");
            }

        }
        public ActionResult Logout()
        {
            Session.Remove("ADMIN");
            return Redirect("/AdminHome/Index");
        }
    }
}
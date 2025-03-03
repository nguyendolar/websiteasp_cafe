using System.Web.Mvc;
using websitecafe.Daos;
using websitecafe.Models; // Import model User

namespace websitecafe.Controllers
{
    public class AuthenticationController : Controller
    {
        UserDao userDao = new UserDao();

        // Hiển thị trang đăng nhập
        public ActionResult Login()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (userDao.CheckLogin(email, password))
            {
                var user = userDao.GetUserByEmail(email);
                Session.Add("USER", user);
                if (user.IsAdmin)
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai email hoặc mật khẩu!";
            return View();
        }

        // Hiển thị trang đăng ký
        public ActionResult Signup()
        {
            return View();
        }

        // Xử lý đăng ký
        [HttpPost]
        public ActionResult Signup(User user)
        {
            if (userDao.AddUser(user))
            {
                TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Email đã tồn tại!";
            return View();
        }

        // Xử lý đăng xuất
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

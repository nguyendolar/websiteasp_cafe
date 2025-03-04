using System.Net.Mail;
using System.Net;
using System;
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

        // Hiển thị trang quên mật khẩu
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // Xử lý gửi email chứa link xác thực
        [HttpPost]
        public ActionResult SendResetLink(string email)
        {
            var user = userDao.GetUserByEmail(email);
            if (user == null)
            {
                ViewBag.Error = "Email không tồn tại!";
                return View("ForgotPassword");
            }

            // Tạo link xác thực
            string verifyLink = Url.Action("VerifyReset", "Authentication", new { email = email }, Request.Url.Scheme);

            // Gửi email
            string subject = "Xác thực quên mật khẩu - Website Cafe";
            string body = $"Nhấp vào link để xác thực: <a href='{verifyLink}'>Xác thực quên mật khẩu</a>";
            SendEmail(email, subject, body);

            ViewBag.Success = "Đã gửi email xác thực!";
            return View("ForgotPassword");
        }

        // Xử lý khi người dùng nhấp vào link xác thực
        public ActionResult VerifyReset(string email)
        {
            var user = userDao.GetUserByEmail(email);
            if (user == null)
            {
                ViewBag.Error = "Email không hợp lệ!";
                return View("ForgotPassword");
            }

            // Cập nhật mật khẩu mặc định
            string newPassword = "123456789";
            userDao.UpdatePassword(email, newPassword);

            // Gửi email chứa mật khẩu mới
            string subject = "Mật khẩu mới của bạn";
            string body = $"Mật khẩu mới của bạn là: {newPassword}";
            SendEmail(email, subject, body);

            ViewBag.Success = "Mật khẩu mới đã được gửi về email!";
            return RedirectToAction("Login");
        }

        // Hàm gửi email
        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("purplerose2305@gmail.com", "vtsvzroezxsrvvze"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("purplerose2305@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi gửi email: " + ex.Message;
            }
        }
    }
}

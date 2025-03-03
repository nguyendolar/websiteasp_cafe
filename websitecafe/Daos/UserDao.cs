using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using websitecafe.Models;

namespace websitecafe.Daos
{
    public class UserDao
    {
        private readonly DBWebcafeContext _context;

        public UserDao()
        {
            _context = new DBWebcafeContext();
        }

        // Thêm người dùng mới
        public bool AddUser(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return false; // Email đã tồn tại

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        // Tìm user theo ID
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // Tìm user theo Email
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        // Lấy danh sách tất cả user
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Cập nhật thông tin user
        public bool UpdateUser(User updatedUser)
        {
            var user = _context.Users.Find(updatedUser.Id);
            if (user == null) return false;

            user.FullName = updatedUser.FullName;
            user.Password = updatedUser.Password;
            user.Email = updatedUser.Email;
            user.IsAdmin = updatedUser.IsAdmin;

            _context.SaveChanges();
            return true;
        }

        // Xóa user theo ID
        public bool DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        // Kiểm tra đăng nhập
        public bool CheckLogin(string email, string password)
        {
            return _context.Users.Any(u => u.Email == email && u.Password == password);
        }
    }

}
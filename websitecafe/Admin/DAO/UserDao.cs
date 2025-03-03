using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using websitecafe.Models;

namespace websitecafe.Admin.DAO
{
	public class UserDao
	{
        DBWebcafeContext myDb = new DBWebcafeContext();
        public bool checkLogin(string userName, string password)
        {
            var obj = myDb.Users.FirstOrDefault(x => x.Email == userName && x.Password == password);
            if (obj == null) { return false; }
            return true;
        }

        public User getUserByEmail(string email)
        {
            return myDb.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public User getInfor(int id)
        {
            return myDb.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> getAdmin() { return myDb.Users.Where(x => x.IsAdmin == true).ToList(); }

        public List<User> getKH() { return myDb.Users.Where(x => x.IsAdmin == false).ToList(); }

        public void add(User user)
        {
            myDb.Users.Add(user);
            myDb.SaveChanges();
        }

        public bool checkExistEmail(string userName)
        {
            var obj = myDb.Users.FirstOrDefault(x => x.Email == userName);
            if (obj != null) { return true; }
            return false;
        }

        public void delete(int id)
        {
            var obj = myDb.Users.FirstOrDefault(x => x.Id == id);
            myDb.Users.Remove(obj);
            myDb.SaveChanges();
        }

        public void update(User user)
        {
            var obj = myDb.Users.FirstOrDefault(x => x.Id == user.Id);
            obj.FullName = user.FullName;
            obj.Email = user.Email;
            myDb.SaveChanges();
        }

        public string md5(string password)
        {
            MD5 md = MD5.Create();
            byte[] inputString = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md.ComputeHash(inputString);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x"));
            }
            return sb.ToString();
        }
    }
}
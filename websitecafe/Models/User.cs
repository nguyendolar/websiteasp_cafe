using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace websitecafe.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required, MaxLength(255)]
        public string Password { get; set; }
        [Required, MaxLength(255)]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
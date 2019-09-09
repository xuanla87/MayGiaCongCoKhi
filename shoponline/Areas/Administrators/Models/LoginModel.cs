using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoponline.Models
{
    public class LoginModel
    {
        public long UserId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string UserPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}
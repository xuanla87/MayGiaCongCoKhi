using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoponline.Models
{
    public class AdminModel
    {
        [Display(Name = "Tên quyền")]
        public string RoleName { get; set; }

        [Display(Name = "Tên người dùng")]
        [Required(ErrorMessage = "Bạn chưa nhập tên người dùng!")]
        [StringLength(50, ErrorMessage = "Tên người dùng nhỏ hơn 50 ký tự!")]
        [MinLength(6, ErrorMessage = "Tên người dùng lớn hơn 6 ký tự!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập email!")]
        [StringLength(250)]
        [Display(Name = "Địa chỉ email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [MinLength(8, ErrorMessage = "Mật khẩu lớn hơn 8 ký tự!")]
        [StringLength(36, ErrorMessage = "Mật khẩu nhỏ hơn 36 ký tự!")]
        [Display(Name = "Mật khẩu")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [MinLength(8, ErrorMessage = "Mật khẩu lớn hơn 8 ký tự!")]
        [StringLength(36, ErrorMessage = "Mật khẩu nhỏ hơn 36 ký tự!")]
        [Display(Name = "Xác minh mật khẩu")]
        public string confirmPassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace shoponline.Models
{
    public class UsersModel
    {
        public long Id { get; set; }

        [Display(Name = "Tên người dùng")]
        [StringLength(36, ErrorMessage = "Tên người dùng không vượt quá 36 ký tự!")]
        [MinLength(6, ErrorMessage = "Tên người dùng không nhỏ hơn 6 ký tự!")]
        [Required(ErrorMessage = "Bạn chưa nhập tên người dùng!")]
        [Remote("IsUserNameExists", "Users", ErrorMessage = "Tên người dùng đã tồn tại!", AdditionalFields = "Id")]
        public string UserName { get; set; }

        [Display(Name = "Địa chỉ email")]
        [StringLength(250, ErrorMessage = "Địa chỉ email không vượt quá 250 ký tự!")]
        [MinLength(6, ErrorMessage = "Địa chỉ email không nhỏ hơn 6 ký tự!")]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ email!")]
        [Remote("IsUserEmailExists", "Users", ErrorMessage = "Địa chỉ email đã tồn tại!", AdditionalFields = "Id")]
        public string UserEmail { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [StringLength(250, ErrorMessage = "Mật khẩu không vượt quá 250 ký tự!")]
        [MinLength(6, ErrorMessage = "Mật khẩu không nhỏ hơn 6 ký tự!")]
        public string UserPassword { get; set; }

        [Display(Name = "Xác minh mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa xác minh mật khẩu!")]
        [System.ComponentModel.DataAnnotations.Compare("UserPassword", ErrorMessage = "Xác minh mật khẩu không đúng, vui lòng nhập lại !")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phân quyền")]
        public long? RoleId { get; set; }

        public bool? Status { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }

        [Display(Name ="Ảnh đại diện")]
        public string UserAvata { get; set; }

        [Display(Name = "Họ tên đầy đủ")]
        public string UserFullName { get; set; }

        [Display(Name = "Giới tính")]
        public string UserGender { get; set; }

        [Display(Name = "Năm sinh")]
        public string UserBirthDay{ get; set; }

        [Display(Name = "Giới thiệu về bạn")]
        public string UserAbout { get; set; }


        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [Remote("IsUserPassExists", "Users", ErrorMessage = "Mật khẩu không đúng!", AdditionalFields = "Id")]
        public string OldPassword { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu mới!")]
        [StringLength(250, ErrorMessage = "Mật khẩu không vượt quá 250 ký tự!")]
        [MinLength(6, ErrorMessage = "Mật khẩu không nhỏ hơn 6 ký tự!")]
        public string NewPassword { get; set; }

        [Display(Name = "Xác minh mật khẩu mới")]
        [Required(ErrorMessage = "Bạn chưa xác minh mật khẩu mới!")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Xác minh mật khẩu mới không đúng, vui lòng nhập lại !")]
        public string NewConfirmPassword { get; set; }

    }
}
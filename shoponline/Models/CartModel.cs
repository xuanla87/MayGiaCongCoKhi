using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ShopOnlineModel.EF;

namespace shoponline.Models
{
    public class CartModel
    {
        public long Id { get; set; }

        public long? UserId { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal? Total { get; set; }

        [StringLength(250)]
        [Display(Name = "Khách hàng")]
        [Required(ErrorMessage = "Bạn chưa nhập người mua hàng!")]
        public string FullName { get; set; }

        [StringLength(250)]
        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Bạn chưa nhập email!")]
        public string Email { get; set; }

        [StringLength(500)]
        [Display(Name = "Địa chỉ nhận hàng")]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ nhận hàng!")]
        public string Address { get; set; }

        public bool? Approval { get; set; }
        public bool? Status { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public DateTime? Date { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public DateTime? Modified { get; set; }

        [StringLength(250)]
        [Display(Name = "Điện thoại liên hệ")]
        [Required(ErrorMessage = "Bạn chưa nhập điện thoại liên hệ!")]
        public string PhoneNumber { get; set; }
        [StringLength(500)]
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }

        public List<tb_Products> ListDetail { get;  set;}
    }
}
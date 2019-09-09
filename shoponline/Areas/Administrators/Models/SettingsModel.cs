using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoponline.Models
{
    public class SettingsModel
    {
        [Display(Name = "Banner")]
        public string Banener { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [StringLength(120, ErrorMessage = "Từ khóa quá dài! nên để từ 90 - 120 ký tự")]
        [Display(Name = "Thẻ từ khóa trang")]
        public string Site_Keywords { get; set; }

        [StringLength(80, ErrorMessage = "Tiêu đề quá dài! nên để từ 70 - 80 ký tự")]
        [Display(Name = "Thẻ tiêu đề trang")]
        public string Site_Title { get; set; }

        [StringLength(200, ErrorMessage = "Mô tả quá dài! nên để từ 20 - 200 ký tự")]
        [MinLength(21, ErrorMessage = "Mô tả quá ngắn! nên để từ 20 - 200 ký tự")]
        [Display(Name = "Thẻ mô tả trang")]
        public string Site_Description { get; set; }

        [Display(Name = "Thông tin trang")]
        public string Infor_Site { get; set; }

        [Display(Name = "Tùy chỉnh")]
        public string Customize_Site { get; set; }

        [Display(Name = "Điều khoản sử dụng")]
        public string Terms_Site { get; set; }

        [Display(Name = "Danh mục chính")]
        public string Main_Menu { get; set; }

        [Display(Name = "Danh mục 01")]
        public string Menu_01 { get; set; }

        [Display(Name = "Danh mục 02")]
        public string Menu_02 { get; set; }

        [Display(Name = "Dịch vụ của chúng tôi")]
        public string Menu_03 { get; set; }

        [Display(Name = "Hướng dẫn khách hàng")]
        public string Menu_04 { get; set; }

        [Display(Name = "Quy định và chính sách")]
        public string Menu_05 { get; set; }

        [Display(Name = "Tiêu đề hỗ trợ")]
        public string Name_Support01 { get; set; }

        [Display(Name = "Số điện thoại hỗ trợ")]
        public string Phone_Support01 { get; set; }

        [Display(Name = "Skype hỗ trợ")]
        public string Skype_Support01 { get; set; }

        [Display(Name = "Yahoo hỗ trợ")]
        public string Yahoo_Support01 { get; set; }

        [Display(Name = "Tiêu đề hỗ trợ")]
        public string Name_Support02 { get; set; }

        [Display(Name = "Số điện thoại hỗ trợ")]
        public string Phone_Support02 { get; set; }

        [Display(Name = "Skype hỗ trợ")]
        public string Skype_Support02 { get; set; }

        [Display(Name = "Yahoo hỗ trợ")]
        public string Yahoo_Support02 { get; set; }

        [Display(Name = "Tiêu đề hỗ trợ")]
        public string Name_Support03 { get; set; }

        [Display(Name = "Số điện thoại hỗ trợ")]
        public string Phone_Support03 { get; set; }

        [Display(Name = "Skype hỗ trợ")]
        public string Skype_Support03 { get; set; }

        [Display(Name = "Yahoo hỗ trợ")]
        public string Yahoo_Support03 { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoponline.Models
{
    public class ProductsModel
    {
        public long Id { get; set; }

        [StringLength(20, ErrorMessage = "Mã sản phẩm quá dài!")]
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập mã cho sản phẩm!")]
        [Remote("IsCodeExists", "Products", ErrorMessage = "Mã sản phẩm đã tồn tại!", AdditionalFields = "Id")]
        public string Code { get; set; }

        [StringLength(250, ErrorMessage = "Tên sản phẩm quá dài!")]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm!")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Đường dẫn quá dài!")]
        [Display(Name = "Đường dẫn chi tiết")]
        [Required(ErrorMessage = "Đường dẫn chưa được nhập!")]
        [Remote("IsLinksExists", "Products", ErrorMessage = "Đường dẫn đã tồn tại!", AdditionalFields = "Id")]
        public string Title { get; set; }

        [Display(Name = "Danh mục sản phẩm")]
        public long? CategoryId { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Thumbnail { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả ngắn")]
        public string Description { get; set; }

        [Display(Name = "Thông số kỹ thuật")]
        public string Infor { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Tổng quan")]
        public string Content { get; set; }

        [Display(Name = "Phần mềm và hướng dẫn")]
        public string Guide { get; set; }


        public DateTime? Date { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime? Modified { get; set; }

        [Display(Name = "Đơn giá")]
        public decimal? Price { get; set; }

        [Display(Name = "Khuyến mại (%)")]
        public int? Sale { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ từ khóa")]
        public string Meta_Keyword { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ tiêu đề")]
        public string Meta_Title { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ mô tả")]
        public string Meta_Description { get; set; }

        public long? UserId { get; set; }

        [StringLength(2)]
        [Display(Name = "Ngôn ngữ")]
        public string LanguageId { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }

       

        [Column(TypeName = "xml")]
        public string Images { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Thời gian bảo hành (tháng)")]
        public int? warranty { get; set; }

        [Display(Name = "Thuế VAT")]
        public bool? IncludedVAT { get; set; }

        [Display(Name = "Ngày lên top")]
        public DateTime? TopHot { get; set; }

        [Display(Name = "Lượt xem")]
        public int? ViewCount { get; set; }

        [Display(Name = "Danh mục sản phẩm")]
        public string CategoryName { get; set; }

        [Display(Name = "Trang chủ")]
        public bool? DisplayHome { get; set; }

        [Display(Name = "Trọn bộ")]
        public bool? FullProduct { get; set; }

    }
}
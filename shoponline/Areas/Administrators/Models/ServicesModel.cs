using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoponline.Models
{
    public class ServicesModel
    {
        public long Id { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề dịch vụ")]
        public string Title { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường dẫn")]
        [Remote("IsLinksExists", "Contents", ErrorMessage = "Đường dẫn đã tồn tại!", AdditionalFields = "Id")]
        public string Slug { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Thumbnail { get; set; }

        [Display(Name = "Chuyên mục")]
        public long? CategoryId { get; set; }

        [Display(Name = "Trang cha")]
        public long? ParentId { get; set; }

        [Display(Name = "Sắp xếp")]
        public int? Order { get; set; }

        public int? View { get; set; }

        public DateTime? Date { get; set; }
        [Display(Name = "Ngày đăng")]
        public DateTime? Modified { get; set; }

        public bool? Status { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public long? UserId { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ tiêu đề")]
        public string Meta_Title { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ từ khóa")]
        public string Meta_Keyword { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ mô tả")]
        public string Meta_Description { get; set; }

        public string Taxonomy { get; set; }
    }
}
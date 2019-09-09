using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoponline.Models
{
    public class CategoryProductsModel
    {

        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên danh mục")]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Đường dẫn danh mục")]
        [Remote("IsLinksExists", "CategoryProducts", ErrorMessage = "Đường dẫn đã tồn tại!", AdditionalFields = "Id")]
        public string Slug { get; set; }

        [Display(Name = "Danh mục cha")]
        public long? ParentId { get; set; }

        [Display(Name = "Sắp xếp")]
        public int? Order { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }

        public long? UserId { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ tiêu đề")]
        public string Meta_Title { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ mô tả")]
        public string Meta_Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Thẻ từ khóa")]
        public string Meta_Keyword { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string Taxonomy { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Thumbnail { get; set; }
    }
}
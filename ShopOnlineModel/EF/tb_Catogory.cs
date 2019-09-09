namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Catogory
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Slug { get; set; }

        public long? ParentId { get; set; }

        public int? Order { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }

        public long? UserId { get; set; }

        [StringLength(250)]
        public string Meta_Title { get; set; }

        [StringLength(250)]
        public string Meta_Description { get; set; }

        [StringLength(250)]
        public string Meta_Keyword { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        public string Thumbnail { get; set; }

        [StringLength(50)]
        public string Taxonomy { get; set; }
    }
}

namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Contents
    {
        public long Id { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Slug { get; set; }

        [StringLength(250)]
        public string Thumbnail { get; set; }

        public long? CategoryId { get; set; }

        public long? ParentId { get; set; }

        public int? Order { get; set; }

        public int? View { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }

        public bool? Status { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? UserId { get; set; }

        [StringLength(250)]
        public string Meta_Title { get; set; }

        [StringLength(250)]
        public string Meta_Keyword { get; set; }

        [StringLength(250)]
        public string Meta_Description { get; set; }

        [StringLength(50)]
        public string Taxonomy { get; set; }
    }
}

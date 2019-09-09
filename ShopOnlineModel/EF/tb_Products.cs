namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Products
    {
        public long Id { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public long? CategoryId { get; set; }

        [StringLength(250)]
        public string Thumbnail { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }

        public decimal? Price { get; set; }

        public int? Sale { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        public string Meta_Keyword { get; set; }

        [StringLength(250)]
        public string Meta_Title { get; set; }

        [StringLength(250)]
        public string Meta_Description { get; set; }

        public long? UserId { get; set; }

        [StringLength(2)]
        public string LanguageId { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "xml")]
        public string Images { get; set; }

        public int? Quantity { get; set; }

        public int? warranty { get; set; }

        public bool? IncludedVAT { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}

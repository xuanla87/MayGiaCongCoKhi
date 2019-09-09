namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Sliders
    {
        public long Id { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public bool? Status { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }

        public long? TypeId { get; set; }

        public int? Order { get; set; }
    }
}

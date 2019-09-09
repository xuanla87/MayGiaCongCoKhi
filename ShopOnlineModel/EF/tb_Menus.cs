namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Menus
    {
        public long Id { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        public int? Order { get; set; }

        public bool? Status { get; set; }

        public long? TypeId { get; set; }

        public long? ParentId { get; set; }
    }
}

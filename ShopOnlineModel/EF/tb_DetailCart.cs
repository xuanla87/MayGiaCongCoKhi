namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_DetailCart
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public long CartId { get; set; }
    }
}

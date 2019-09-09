namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Carts
    {
        public long Id { get; set; }

        public long? UserId { get; set; }

        public decimal? Total { get; set; }

        [StringLength(250)]
        public string FullName { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public bool? Approval { get; set; }
        public bool? Status { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(250)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        
    }
}

namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Contact
    {
        public long Id { get; set; }

        public long? UserId { get; set; }

        [StringLength(250)]
        public string FullName { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(1000)]
        public string Content { get; set; }

        public bool? Status { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }
    }
}

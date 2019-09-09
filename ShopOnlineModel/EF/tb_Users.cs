namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Users
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(250)]
        public string UserPassword { get; set; }

        public bool? Status { get; set; }

        public long? RoleId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Modified { get; set; }
    }
}

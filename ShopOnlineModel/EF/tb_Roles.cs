namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Roles
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        [Column(TypeName = "ntext")]
        public string RoleDescription { get; set; }

        public int? RoleAction { get; set; }
    }
}

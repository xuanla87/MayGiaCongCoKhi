namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_MetaUser
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        [StringLength(50)]
        public string Key { get; set; }

        [Column(TypeName = "ntext")]
        public string Value { get; set; }
    }
}

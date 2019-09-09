namespace ShopOnlineModel.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Languages
    {
        [Key]
        [StringLength(2)]
        public string LanguageId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}

namespace ShopOnlineModel.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopModelDbContext : DbContext
    {
        public ShopModelDbContext()
            : base("name=ShopModelEntities")
        {
        }

        public virtual DbSet<tb_Carts> tb_Carts { get; set; }
        public virtual DbSet<tb_Catogory> tb_Catogory { get; set; }
        public virtual DbSet<tb_Contact> tb_Contact { get; set; }
        public virtual DbSet<tb_Contents> tb_Contents { get; set; }
        public virtual DbSet<tb_DetailCart> tb_DetailCart { get; set; }
        public virtual DbSet<tb_Languages> tb_Languages { get; set; }
        public virtual DbSet<tb_Menus> tb_Menus { get; set; }
        public virtual DbSet<tb_MenuType> tb_MenuType { get; set; }
        public virtual DbSet<tb_MetaContent> tb_MetaContent { get; set; }
        public virtual DbSet<tb_MetaProduct> tb_MetaProduct { get; set; }
        public virtual DbSet<tb_MetaUser> tb_MetaUser { get; set; }
        public virtual DbSet<tb_Products> tb_Products { get; set; }
        public virtual DbSet<tb_Roles> tb_Roles { get; set; }
        public virtual DbSet<tb_Settings> tb_Settings { get; set; }
        public virtual DbSet<tb_Sliders> tb_Sliders { get; set; }
        public virtual DbSet<tb_SliderType> tb_SliderType { get; set; }
        public virtual DbSet<tb_Users> tb_Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_Carts>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tb_Catogory>()
                .Property(e => e.Taxonomy)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Contents>()
                .Property(e => e.Taxonomy)
                .IsUnicode(false);

            modelBuilder.Entity<tb_DetailCart>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tb_Languages>()
                .Property(e => e.LanguageId)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Products>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Products>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tb_Products>()
                .Property(e => e.LanguageId)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Settings>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Sliders>()
                .Property(e => e.Target)
                .IsUnicode(false);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ModelFE.FE
{
    public partial class NguyenVanHaiContext : DbContext
    {
        public NguyenVanHaiContext()
            : base("name=NguyenVanHaiContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.IDCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitCost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.UserName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.PassWord)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}

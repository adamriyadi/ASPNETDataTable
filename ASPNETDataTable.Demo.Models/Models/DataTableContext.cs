namespace ASPNETDataTable.Demo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entities;

    public partial class DataTableContext : DbContext
    {
        public DataTableContext()
            : base("name=DefaultConnectionSqlServer")
        {
        }

        public virtual DbSet<Customers> customers { get; set; }
        public virtual DbSet<Orders> orders { get; set; }
        public virtual DbSet<Products> products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .ToTable("customers");

            modelBuilder.Entity<Products>()
                .ToTable("products");

            modelBuilder.Entity<Orders>()
                .ToTable("orders");

            modelBuilder.Entity<Customers>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<Customers>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.qty)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Orders>()
                .Property(e => e.totalprice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Products>()
                .Property(e => e.productname)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.price)
                .HasPrecision(18, 0);
        }
    }
}

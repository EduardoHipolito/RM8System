using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using Stock.Domain;
using Stock.Domain.Configurations;
using System;

//server=manny.db.elephantsql.com;user id=rhlfvkzb;password=fSw2k7MWJ4jLrm_F1-k5duJHmZ22v9Yx;database=rhlfvkzb
//Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DBStock.mdf;Integrated Security = True;Connect Timeout = 30;User Instance = True
//Server = .\;Initial Catalog=DBStock; AttachDbFilename=|DataDirectory|\DBStock.mdf; Trusted_Connection = True; MultipleActiveResultSets = true
//Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-TheHouse-20150330082525.mdf;Initial Catalog=aspnet-TheHouse-20150330082525;Integrated Security=True
//Server=localhost\SQLEXPRESS;Database=DBCore;User Id=sa;Password=93298440
//Server=localhost\SQLEXPRESS;Database=DBStock_Test;User Id=sa;Password=93298440
//Server=LAPTOP-VC2FBTM3\SQLEXPRESS;Database=RM8_Stock;User Id=RM8_USER;Password=RM8_USER
//Data Source=SQL5045.site4now.net;Initial Catalog=DB_A4DF0B_EduardoHipolito;User Id=DB_A4DF0B_EduardoHipolito_admin;Password=2007Dudu;
//@"Data Source=sql7005.site4now.net;Initial Catalog=DB_A3D636_RM8;User Id=DB_A3D636_RM8_admin;Password=rm8bluto;Connect Timeout = 180"

namespace Stock.DataAccess
{
    public class StockContext : ContextBase
    {
        private const string connection = @"Data Source=198.38.83.200;Initial Catalog=dstudioc_rm8stock_preprod;User Id=dstudioc_rm8;Password=!s$@JFjm$3f!E2#n;Connect Timeout = 180";

        public StockContext() : base(connection, DataBaseType.SqlServer)
        { }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductSale> ProductSale { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<ProductEntry> ProductEntry { get; set; }
        public DbSet<Stock.Domain.Stock> Stock { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<FormOfPayment> FormOfPayment { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Sale> Sale { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CategoryConfiguration(modelBuilder.Entity<Category>());
            new ProductSaleConfiguration(modelBuilder.Entity<ProductSale>());
            new ProductConfiguration(modelBuilder.Entity<Product>());
            new SupplierConfiguration(modelBuilder.Entity<Supplier>());
            new EntryConfiguration(modelBuilder.Entity<Entry>());
            new ProductEntryConfiguration(modelBuilder.Entity<ProductEntry>());
            new StockConfiguration(modelBuilder.Entity<Stock.Domain.Stock>());
            new PaymentConfiguration(modelBuilder.Entity<Payment>());
            new FormOfPaymentConfiguration(modelBuilder.Entity<FormOfPayment>());
            new SaleConfiguration(modelBuilder.Entity<Sale>());
            new CustomerConfiguration(modelBuilder.Entity<Customer>());

            base.OnModelCreating(modelBuilder);
        }
    }
}

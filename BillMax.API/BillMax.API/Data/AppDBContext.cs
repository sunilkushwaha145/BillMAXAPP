
using Microsoft.EntityFrameworkCore;

namespace BillMax.API.Models.Tables
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<InvoiceItems> InvoiceItems { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<StoreProfiles> StoreProfiles { get; set; }
        public DbSet<GSTSetting> GSTSetting { get; set; }
        public DbSet<HSNMaster> HSNMaster { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("Admins");
        }
       
    }
}

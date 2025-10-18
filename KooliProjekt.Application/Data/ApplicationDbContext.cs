using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Invoice_Line> Invoice_lines { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-KooliProjekt-Gunnar;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Order_Item>(entity =>
            {
                entity.Property(e => e.Total).HasPrecision(18, 2);
                entity.Property(e => e.UnitPrice).HasPrecision(10, 2);  
                entity.Property(e => e.Discount).HasPrecision(3, 2);   
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Discount).HasPrecision(3, 2);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Discount).HasPrecision(3, 2);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Discount).HasPrecision(3, 2);
                entity.Property(e => e.Paid).HasPrecision(10, 2);
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Invoice_Line>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasPrecision(10, 2);
                entity.Property(e => e.Discount).HasPrecision(3, 2);
                entity.Property(e => e.Total).HasPrecision(20, 2);
            });
        }
    }
}

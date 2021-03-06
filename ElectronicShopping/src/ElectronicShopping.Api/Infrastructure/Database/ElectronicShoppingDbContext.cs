using ElectronicShopping.Api.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectronicShopping.Api.Infrastructure.Database
{
    public class ElectronicShoppingDbContext : DbContext
    {
        public ElectronicShoppingDbContext(DbContextOptions<ElectronicShoppingDbContext> options)
       : base(options)
        {

        }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ItemEntity> Items { get; set; }
        public virtual DbSet<StockEntity> Stocks { get; set; }
        public virtual DbSet<CartEntity> Baskets { get; set; }
        public virtual DbSet<CartDetailEntity> BasketDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<ItemEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<StockEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Item)
                 .WithMany(x => x.Stocks)
                 .HasForeignKey(x => x.ItemId);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<CartEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(p => p.RowVersion).IsRowVersion();
            });

            modelBuilder.Entity<CartDetailEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Cart)
                 .WithMany(x => x.CartDetails)
                 .HasForeignKey(x => x.CartId);
                e.HasOne(x => x.Item)
                 .WithMany(x => x.CartDetails)
                 .HasForeignKey(x => x.ItemId);
                e.Property(p => p.RowVersion).IsRowVersion();
            });
        }
    }
}

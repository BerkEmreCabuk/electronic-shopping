using Microsoft.EntityFrameworkCore;

namespace ElectronicShopping.Api.Repositories
{
    public class ElectronicShoppingDbContext : DbContext
    {
        public ElectronicShoppingDbContext(DbContextOptions<ElectronicShoppingDbContext> options)
       : base(options)
        {

        }
        //public DbSet<UrlConvertionEntity> UrlConvertions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

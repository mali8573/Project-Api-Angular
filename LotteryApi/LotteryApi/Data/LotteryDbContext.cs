using LotteryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LotteryApi.Data
{
    public class LotteryDbContext : DbContext
    {
        public  LotteryDbContext(DbContextOptions<LotteryDbContext> options) :base(options){}
        public DbSet <DonorModel> Donors { get; set; }
        public DbSet<GiftModel> Gifts { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PackageModel> Packages { get; set; }
        public DbSet<GiftInOrderModel> GiftsInOrder { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<GiftInCartModel> GiftsInCart { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }
        public DbSet<WinnerModel> Winners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.Packages)
                .WithMany(); 
        }
    }
}

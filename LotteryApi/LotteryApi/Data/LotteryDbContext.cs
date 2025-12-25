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
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<PackageInCartModel> PackagesInCart { get; set; }
        public DbSet<PackageInOrderModel> PackagesInOrder { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // פתרון לשגיאת ה-Cascade במתנות שבתוך חבילה בסל
            modelBuilder.Entity<GiftInCartModel>()
                .HasOne(g => g.PackageInCart)
                .WithMany(p => p.GiftsInPackage)
                .HasForeignKey(g => g.PackageInCartId)
                .OnDelete(DeleteBehavior.Restrict); // מונע מחיקה כפולה בשרשרת

            // פתרון למניעת שגיאה דומה במתנות שבתוך חבילה בהזמנה
            modelBuilder.Entity<GiftInOrderModel>()
                .HasOne(g => g.PackageInOrder)
                .WithMany(p => p.GiftsInPackage)
                .HasForeignKey(g => g.PackageInOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

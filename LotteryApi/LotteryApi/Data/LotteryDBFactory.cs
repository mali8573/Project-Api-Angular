using Microsoft.EntityFrameworkCore;

namespace LotteryApi.Data
{
    public class LotteryDBFactory
    {
        private const string ConnectionString = "Server=DESKTOP-1L8084V\\SQLEXPRESS;DataBase=LotteryDB;Integrated Security=SSPI;" +

    "Persist Security Info=False;TrustServerCertificate=true";

        public static LotteryDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LotteryDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new LotteryDbContext(optionsBuilder.Options);
        }
    }
}

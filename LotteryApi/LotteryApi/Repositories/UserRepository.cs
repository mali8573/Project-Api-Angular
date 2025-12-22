using LotteryApi.Data;
using LotteryApi.Models;

namespace LotteryApi.Repositories
{
    public class UserRepository
    {
        private readonly LotteryDbContext _lotteryContext=LotteryDBFactory.CreateContext();
        public List<UserModel> GetUsers()
        {
            return _lotteryContext.Users.ToList();
        }
    }
}

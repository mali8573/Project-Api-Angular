using LotteryApi.Models;
using LotteryApi.Repositories;

namespace LotteryApi.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository=new();
        public List<UserModel> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}

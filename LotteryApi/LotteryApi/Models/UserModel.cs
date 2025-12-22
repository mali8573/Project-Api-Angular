using LotteryApi.Enums;

namespace LotteryApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public UserRoleEnum Role { get; set; } = UserRoleEnum.Participant;

    }
}

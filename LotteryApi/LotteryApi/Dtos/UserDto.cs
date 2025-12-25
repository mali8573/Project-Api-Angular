using LotteryApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public UserRoleEnum Role { get; set; } = UserRoleEnum.Participant;
    }
}

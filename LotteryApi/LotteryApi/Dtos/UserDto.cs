using LotteryApi.Enums;
using LotteryApi.Models;
using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public string Email { get; set; }
      
        public string Phone { get; set; }
        public string Address { get; set; }
       
        public UserRoleEnum Role { get; set; } = UserRoleEnum.Participant;
        public ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
    }
    public class UserCreateDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        
    }
    public class UserUpdateDto
    {

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, Phone]
        public string? Phone { get; set; }
        [Required]
        public string? Address { get; set; }
       
    }
    public class GiftPurchaserDto
    {
        public int Id { get; set; }           
        public string ParticipantName { get; set; } 
        public string ParticipantPhone { get; set; }
        public bool IsWinner { get; set; } = false;
        public string ParticipantEmail{ get; set; }
    }
}

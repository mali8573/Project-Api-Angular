using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Models
{
    public class DonorModel
    {
        public int Id { get; set; }
        public string? Tz { get; set; }
        
        public string Name { get; set; }
        [Required,Phone]
        public string Phone { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        public ICollection<GiftModel> Gifts { get; set; } = new List<GiftModel>();
      
    }
}

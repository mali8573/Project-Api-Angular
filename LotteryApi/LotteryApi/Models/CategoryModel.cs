using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<GiftModel> Gifts { get; set; } = new List<GiftModel>();

    }
}

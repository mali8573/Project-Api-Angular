using LotteryApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryApi.Models
{
    public class GiftModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryEnum Category { get; set; }
        public int CardAmount { get; set; }
        public CardPriceEnum CardPrice { get; set; }
        public string? PictureUrl { get; set; }
        public int DonorId { get; set; }
        [ForeignKey("DonorId")]
        public DonorModel Donor { get; set; }
       public  ICollection<GiftInOrderModel> Cards { get; set; } = new List<GiftInOrderModel>();
    }
}
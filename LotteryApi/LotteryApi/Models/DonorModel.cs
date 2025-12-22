namespace LotteryApi.Models
{
    public class DonorModel
    {
        public int Id { get; set; }
        public string? Tz { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public ICollection<GiftModel> Gifts { get; set; } = new List<GiftModel>();
    }
}

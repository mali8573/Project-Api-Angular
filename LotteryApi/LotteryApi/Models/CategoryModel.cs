namespace LotteryApi.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GiftModel> Gifts { get; set; } = new List<GiftModel>();

    }
}

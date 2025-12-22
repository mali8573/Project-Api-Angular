namespace LotteryApi.Models
{
    public class PackageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int QtyClassicCards { get; set; }
        public int QtySpecialCards { get; set; }
        public int QtyPrimumCards { get; set; }
       
    }
}

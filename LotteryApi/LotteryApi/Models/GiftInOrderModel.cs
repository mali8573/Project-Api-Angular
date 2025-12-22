using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryApi.Models
{
    public class GiftInOrderModel
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        [ForeignKey("GiftId")]
        public GiftModel Gift { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderModel Order { get; set; }

    }
}

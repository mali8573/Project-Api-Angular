using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryApi.Models
{
    public class GiftInCartModel
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        [ForeignKey("GiftId")]
        public GiftModel Gift { get; set; }
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public ShoppingCartModel ShoppingCart { get; set; }
        public int Qty { get; set; }
    }
}

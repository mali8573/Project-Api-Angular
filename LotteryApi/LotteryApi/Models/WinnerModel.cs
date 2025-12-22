using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryApi.Models
{
    public class WinnerModel
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        [ForeignKey("ParticipantId")]
        public UserModel Participant { get; set; }
        public int GiftId { get; set; }
        [ForeignKey("GiftId")]
        public GiftModel Gift { get; set; }

    }
}

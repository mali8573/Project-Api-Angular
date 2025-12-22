using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryApi.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        [ForeignKey("ParticipantId")]
        public UserModel Participant { get; set; }
        public ICollection<PackageModel> Packages { get; set; } = new List<PackageModel>();
        public int SumPrice { get; set; }
        public DateOnly date {  get; set; }
    }
}

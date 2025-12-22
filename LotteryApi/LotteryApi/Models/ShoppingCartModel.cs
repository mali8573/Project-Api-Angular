using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryApi.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        [ForeignKey("ParticipantId")]
        public UserModel Participant { get; set; }
    }
}

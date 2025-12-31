using LotteryApi.Data;
using LotteryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LotteryApi.Repositories
{
    public class GiftInOrderRepositorycs
    {
        private readonly LotteryDbContext _lotteryContext = LotteryDBFactory.CreateContext();
        public async Task<GiftInOrderModel?> GetGiftInOrderByIdAsync(int id)
        {
            return await _lotteryContext.GiftsInOrder
            .Include(g => g.Gift)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

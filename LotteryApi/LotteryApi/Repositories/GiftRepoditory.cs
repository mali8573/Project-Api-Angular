using LotteryApi.Data;
using LotteryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LotteryApi.Repositories
{
    public class GiftRepoditory
    {
        private readonly LotteryDbContext _lotteryContext = LotteryDBFactory.CreateContext();
        public async Task<IEnumerable<GiftModel>> GetGiftsAsync()
        {
            return await _lotteryContext.Gifts
                .Include(c => c.Category)
                .Include(d=>d.Donor)
                .ToListAsync();
        }
        public async Task<GiftModel?> GetGiftByIdAsync(int id)
        {
            return await _lotteryContext.Gifts
                .Include(c => c.Category)
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<GiftModel> CreateGiftAsync(GiftModel gift)
        {
            _lotteryContext.Gifts.Add(gift);
            await _lotteryContext.SaveChangesAsync();
            return gift;
        }
        public async Task<GiftModel?> UpdateGiftAsync(GiftModel gift)
        {
            var existing = await _lotteryContext.Gifts.FindAsync(gift.Id);
            if (existing == null)
                return null;
            _lotteryContext.Entry(existing).CurrentValues.SetValues(gift);
            await _lotteryContext.SaveChangesAsync();
            return existing;

        }
        public async Task<bool> DeleteGiftAsync(int id)
        {
            var existing = await _lotteryContext.Gifts.FindAsync(id);
            if (existing == null)
                return false;
            _lotteryContext.Gifts.Remove(existing);
            await _lotteryContext.SaveChangesAsync();
            return true;
        }

    }
}

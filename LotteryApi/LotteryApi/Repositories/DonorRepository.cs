using LotteryApi.Data;
using LotteryApi.Dtos;
using LotteryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LotteryApi.Repositories
{
    public class DonorRepository
    {
        private readonly LotteryDbContext _lotteryContext = LotteryDBFactory.CreateContext();
        public async Task <IEnumerable<DonorModel>> GetDonorsAsync()
        {
            return await _lotteryContext.Donors.ToListAsync();
        }
        public async Task<DonorModel?> GetDonorsByIdAsync(int id)
        {
            return await _lotteryContext.Donors.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<DonorModel> CreateDonorsAsync(DonorModel donor)
        {
            _lotteryContext.Donors.Add(donor);
            await _lotteryContext.SaveChangesAsync();
            return donor;
        }
        public async Task<DonorModel?> UpdateDonorsAsync(DonorModel donor)
        {
            var existing = await _lotteryContext.Donors.FindAsync(donor.Id);
            if (existing == null)
                return null;
            _lotteryContext.Entry(existing).CurrentValues.SetValues(donor);
            await _lotteryContext.SaveChangesAsync();
            return existing;

        }
        public async Task<bool> DeleteDonorsAsync(int id)
        {
            var existing = await _lotteryContext.Donors.FindAsync(id);
            if (existing == null)
                return false;
            _lotteryContext.Donors.Remove(existing);
            await _lotteryContext.SaveChangesAsync();
            return true;
        }
    }

}

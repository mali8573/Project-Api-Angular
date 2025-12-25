using LotteryApi.Dtos;
using LotteryApi.Models;
using LotteryApi.Repositories;

namespace LotteryApi.Services
{
    public class DonorService
    {
        private readonly DonorRepository _donorRepository = new();
        public async  Task<IEnumerable<DonorDto>> GetDonorsAsync()
        {
            var donors = await _donorRepository.GetDonorsAsync();
            return donors.Select(d=>new DonorDto
            {
                Name = d.Name,  
                Tz = d.Tz,
                Phone = d.Phone,
                Email = d.Email,


            });
        }
        public async Task<DonorDto?> GetDonorsByIdAsync(int id)
        {
            var donor = await _donorRepository.GetDonorsByIdAsync(id);
            return donor != null ? new DonorDto { Tz = donor.Tz, Name = donor.Name, Email = donor.Email, Phone = donor.Phone }:null;
        }

        public async Task<DonorDto> CreateDonorsAsync(DonorDto donor)
        {
            var newDonor = new DonorModel
            {
                Tz = donor.Tz,
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone,
                
            };

            var createDonor=await _donorRepository.CreateDonorsAsync(newDonor);
            return new DonorDto {Tz=createDonor.Tz,Name=createDonor.Name,Email=createDonor.Email,Phone=createDonor.Phone };
        }

        public async Task<DonorDto?> UpdateDonorsAsync(int id, DonorUpdateDto updateDonor)
        {
            var existing = await _donorRepository.GetDonorsByIdAsync(id);
            if (existing == null)
            {
                return null;
            }
            if (updateDonor.Tz != null) existing.Tz = updateDonor.Tz;
            if (updateDonor.Name!=null)existing.Name = updateDonor.Name; 
            if(updateDonor.Email!=null)existing.Email = updateDonor.Email;
            if (updateDonor.Phone!=null) existing.Phone = updateDonor.Phone;
            var newUpdateDonor = await _donorRepository.UpdateDonorsAsync(existing);
            return newUpdateDonor != null ? new DonorDto { Tz = newUpdateDonor.Tz, Name = newUpdateDonor.Name, Email = newUpdateDonor.Email, Phone = newUpdateDonor.Phone } : null;
        }
        public async Task<bool> DeleteDonorsAsync(int id)
        {
            return await _donorRepository.DeleteDonorsAsync(id);
        }
    }
}

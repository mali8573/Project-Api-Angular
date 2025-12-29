using LotteryApi.Dtos;
using LotteryApi.Models;
using LotteryApi.Repositories;

namespace LotteryApi.Services
{
    public class GiftService
    {
        private readonly GiftRepoditory _giftRepository = new();
        public async Task<IEnumerable<GiftDto>> GetGiftsAsync()
        {
            var gifts = await _giftRepository.GetGiftsAsync();
            return gifts.Select(g => new GiftDto
            {
                Id = g.Id,
                Name = g.Name,
               Description = g.Description,
               CategoryId = g.CategoryId,
               DonorId = g.DonorId,
               CategoryName = g.Category!= null ? g.Category.Name : null,
               DonorName = g.Donor != null ? g.Donor.Name : null,
               PrizeQuantity = g.PrizeQuantity,
               CardPrice = g.CardPrice.ToString(),
               PictureUrl = g.PictureUrl

            });

        }
        public async Task<GiftDto?> GetGiftByIdAsync(int id)
        {
            var gift = await _giftRepository.GetGiftByIdAsync(id);
            return gift != null ? new GiftDto 
            { Id = gift.Id,
                Name = gift.Name,
                Description = gift.Description,
                CategoryId = gift.CategoryId,
                DonorId = gift.DonorId,
                CategoryName = gift.Category != null ? gift.Category.Name : null,
                DonorName = gift.Donor != null ? gift.Donor.Name : null,
                PrizeQuantity = gift.PrizeQuantity,
                CardPrice = gift.CardPrice.ToString(),
                PictureUrl = gift.PictureUrl

            } : null;
        }

        public async Task<GiftDto> CreateGiftAsync(GiftCreateDto gift)
        {
            var newGift = new GiftModel()
            {
                Name = gift.Name,
                Description = gift.Description,
                CategoryId = gift.CategoryId,
                DonorId = gift.DonorId,
                PrizeQuantity = gift.PrizeQuantity,
                CardPrice = gift.CardPrice,
                PictureUrl = gift.PictureUrl
            };

            var createGift = await _giftRepository.CreateGiftAsync(newGift);
            var giftWithDetails =  await _giftRepository.GetGiftByIdAsync(createGift.Id);
            return new GiftDto 
            {
                Id = giftWithDetails.Id,
                Name = giftWithDetails.Name,
                Description = giftWithDetails.Description,
                CategoryId = giftWithDetails.CategoryId,
                DonorId = giftWithDetails.DonorId,
                CategoryName = giftWithDetails.Category != null ? giftWithDetails.Category.Name : null,
                DonorName = giftWithDetails.Donor != null ? giftWithDetails.Donor.Name : null,
                PrizeQuantity = giftWithDetails.PrizeQuantity,
                CardPrice = giftWithDetails.CardPrice.ToString(),
                PictureUrl = giftWithDetails.PictureUrl
            };
        }

        public async Task<GiftDto?> UpdateGiftAsync(int id, GiftUpdateDto updateGift)
        {
            var existing = await _giftRepository.GetGiftByIdAsync(id);
            if (existing == null)
            {
                return null;
            }
            if (updateGift.Name != null) existing.Name = updateGift.Name;
            if (updateGift.Description != null) existing.Description = updateGift.Description;
            existing.CategoryId = updateGift.CategoryId ?? existing.CategoryId;
            existing.DonorId = updateGift.DonorId ?? existing.DonorId;
            existing.PrizeQuantity = updateGift.PrizeQuantity ?? existing.PrizeQuantity;
            existing.CardPrice = updateGift.CardPrice ?? existing.CardPrice;
            if (updateGift.PictureUrl != null) existing.PictureUrl = updateGift.PictureUrl;
            var newUpdateGift = await _giftRepository.UpdateGiftAsync(existing);
            var giftWithDetails = await _giftRepository.GetGiftByIdAsync(newUpdateGift.Id);
            return new GiftDto
            {
                Id = giftWithDetails.Id,
                Name = giftWithDetails.Name,
                Description = giftWithDetails.Description,
                CategoryId = giftWithDetails.CategoryId,
                DonorId = giftWithDetails.DonorId,
                CategoryName = giftWithDetails.Category != null ? giftWithDetails.Category.Name : null,
                DonorName = giftWithDetails.Donor != null ? giftWithDetails.Donor.Name : null,
                PrizeQuantity = giftWithDetails.PrizeQuantity,
                CardPrice = giftWithDetails.CardPrice.ToString(),
                PictureUrl = giftWithDetails.PictureUrl
            };

        }
        public async Task<bool> DeleteGiftAsync(int id)
        {
            return await _giftRepository.DeleteGiftAsync(id);
        }

    }
}

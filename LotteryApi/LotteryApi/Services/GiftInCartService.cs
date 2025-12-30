using LotteryApi.Dtos;
using LotteryApi.Models;
using LotteryApi.Repositories;

namespace LotteryApi.Services
{
    public class GiftInCartService
    {
        private readonly GiftInCartRepository _giftInCartRepository = new();
        private readonly PackageInCartRepository _PackageInCartRepository = new();

        public async Task<GiftInCartDto?> GetGiftInCartByIdAsync(int id)
        {
            var giftInCart = await _giftInCartRepository.GetGiftInCartByIdAsync(id);
            return giftInCart != null ? new GiftInCartDto
                  {
                Id = giftInCart.Id,
                GiftId = giftInCart.GiftId,
                GiftName = giftInCart.Gift?.Name,
                giftPictureUrl = giftInCart.Gift?.PictureUrl,
                giftCardPrice = giftInCart.Gift?.CardPrice.ToString(),
                Qty = giftInCart.Qty
            } : null;
           
        }
        public async Task<GiftInCartDto?> GetGiftInCartByIdAndByPackageAsync(int giftid, int packageInCartId)
        {

            var giftInCart = await _giftInCartRepository.GetGiftInCartByIdAndByPackageAsync(giftid, packageInCartId);

                  return giftInCart != null ? new GiftInCartDto
                  {
                      Id = giftInCart.Id,
                      GiftId = giftInCart.GiftId,
                      GiftName = giftInCart.Gift?.Name,
                      giftPictureUrl = giftInCart.Gift?.PictureUrl,
                      giftCardPrice = giftInCart.Gift?.CardPrice.ToString(),
                      Qty = giftInCart.Qty
                  } : null;
        }

        public async Task<GiftInCartDto> CreateOrUpdateGiftInCartAsync(GiftInCartCreateDto giftInCart)
        {
            var existing = await _giftInCartRepository.GetGiftInCartByIdAndByPackageAsync(giftInCart.GiftId, giftInCart.PackageInCartId);
            if (existing == null)
            {
                var package=await _PackageInCartRepository.GetPackageInCartByIdAsync(giftInCart.PackageInCartId);
                if (package == null)
                    return null;
                var newGiftInCart = new GiftInCartModel()
                {
                    PackageInCartId = giftInCart.PackageInCartId,
                    GiftId = giftInCart.GiftId,
                    Qty = giftInCart.Qty,
                    CartId= package.CartId
                };

                var createGiftInCart = await _giftInCartRepository.CreateGiftInCartAsync(newGiftInCart);
                return new GiftInCartDto
                {
                    Id = createGiftInCart.Id,
                    GiftId = createGiftInCart.GiftId,
                    GiftName = createGiftInCart.Gift?.Name,
                    giftPictureUrl = createGiftInCart.Gift?.PictureUrl,
                    giftCardPrice = createGiftInCart.Gift?.CardPrice.ToString(),
                    Qty = createGiftInCart.Qty
                };
            }
            else
            {
                if (existing.Qty + giftInCart.Qty > 0)
                {
                    existing.Qty += giftInCart.Qty;
                    var updatedGiftInCart = await _giftInCartRepository.UpdateGiftAsync(existing);
                    return new GiftInCartDto
                    {
                        Id = updatedGiftInCart.Id,
                        GiftId = updatedGiftInCart.GiftId,
                        GiftName = updatedGiftInCart.Gift?.Name,
                        giftPictureUrl = updatedGiftInCart.Gift?.PictureUrl,
                        giftCardPrice = updatedGiftInCart.Gift?.CardPrice.ToString(),
                        Qty = updatedGiftInCart.Qty
                    };
                }
                else
                {
                    await _giftInCartRepository.DeleteGiftInCartAsync(existing.Id);
                    return null;
                }
            }
           
        }     
        public async Task<bool> DeleteGiftInCartAsync(int id)
        {
            return await _giftInCartRepository.DeleteGiftInCartAsync(id);
        }
    }
}

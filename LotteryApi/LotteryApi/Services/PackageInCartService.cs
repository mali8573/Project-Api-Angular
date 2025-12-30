using LotteryApi.Dtos;
using LotteryApi.Models;
using LotteryApi.Repositories;

namespace LotteryApi.Services
{
    public class PackageInCartService
    {
        private readonly PackageInCartRepository _packageInCartRepository = new();

        public async Task<PackageInCartDto?> GetPackageInCartByIdAsync(int id)
        {
            var packageInCart = await _packageInCartRepository.GetPackageInCartByIdAsync(id);
            return packageInCart != null ? new PackageInCartDto 
            { Id = packageInCart.Id,
            PackageId = packageInCart.PackageId,
            PackageName = packageInCart.Package?.Name,
            PackagePrice = packageInCart.Package?.Price??0,
                GiftsInPackage= packageInCart.GiftsInPackage?.Select(g => new GiftInCartDto
                {
                    Id = g.Id,
                    GiftId = g.GiftId,
                    GiftName = g.Gift ?.Name  ,
                    giftPictureUrl=g.Gift?.PictureUrl,
                    giftCardPrice= g.Gift?.CardPrice.ToString(),
                    Qty = g.Qty
                }).ToList()??[] 
            } : null;
        }

        public async Task<PackageInCartDto> CreatePackageInCartAsync(PackageInCartCreateDto packageInCart)
        {
            var newPackageInCart = new PackageInCartModel()
            {
                PackageId= packageInCart.PackageId

            };

            var createPackageInCart = await _packageInCartRepository.CreatePackageInCartAsync(newPackageInCart);
            return new PackageInCartDto
            {
                Id = createPackageInCart.Id,
                PackageId = createPackageInCart.PackageId,
                PackageName = createPackageInCart.Package?.Name,
                PackagePrice = createPackageInCart.Package?.Price ?? 0,
                GiftsInPackage = createPackageInCart.GiftsInPackage?.Select(g => new GiftInCartDto
                {
                    Id = g.Id,
                    GiftId = g.GiftId,
                    GiftName = g.Gift?.Name,
                    giftPictureUrl = g.Gift?.PictureUrl,
                    giftCardPrice = g.Gift?.CardPrice.ToString(),
                    Qty = g.Qty
                }).ToList() ?? []
            };

        }
        public async Task<bool> DeletePackageInCartAsync(int id)
        {
            return await _packageInCartRepository.DeletePackageInCartAsync(id);
        }
    }
}

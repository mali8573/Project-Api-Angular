using LotteryApi.Dtos;
using LotteryApi.Models;
using LotteryApi.Repositories;

namespace LotteryApi.Services
{
    public class PackageInCartService
    {
        private readonly PackageInCartRepository _packageInCartRepository = new();
        private readonly PackageRepository _packageRepository = new();
        private readonly ShoppingCartRepository _ShoppingCartRepository = new();

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
            var package = await _packageRepository.GetPackageByIdAsync(packageInCart.PackageId);
            if (package == null)
                return null;
             var newPackageInCart = new PackageInCartModel()
            {
                PackageId = packageInCart.PackageId,
                //לשנות השרת צריך מעצמו לדעת
                CartId = 1

            };

            var createPackageInCart = await _packageInCartRepository.CreatePackageInCartAsync(newPackageInCart);
            var cart = await _ShoppingCartRepository.GetShoppingCartByIdAsync(1);
            if (cart != null)
            {
                cart.SumPrice += package.Price;
                await _ShoppingCartRepository.UpdateShoppingCartAsync(cart);
            }
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
            var packageInCart = await _packageInCartRepository.GetPackageInCartByIdAsync(id);
            if (packageInCart == null)
                return false;
            var price=packageInCart?.Package?.Price??0;
            var cart = await _ShoppingCartRepository.GetShoppingCartByIdAsync(1);
            if (cart != null)
            {
                cart.SumPrice -= price;
                await _ShoppingCartRepository.UpdateShoppingCartAsync(cart);
            }

            return await _packageInCartRepository.DeletePackageInCartAsync(id);
          

        }
    }
}

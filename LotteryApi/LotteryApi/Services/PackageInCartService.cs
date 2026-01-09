using LotteryApi.Dtos;
using LotteryApi.Models;
using LotteryApi.Repositories;
using System.Security.Claims;

namespace LotteryApi.Services
{
    public class PackageInCartService
    {
        private readonly PackageInCartRepository _packageInCartRepository = new();
        private readonly PackageRepository _packageRepository = new();
        private readonly ShoppingCartRepository _ShoppingCartRepository = new();
        private readonly IHttpContextAccessor _httpContextAccessor;
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
            var userIdCalm = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdCalm, out int userId))
            {
                return null;
            }
            var cart = await _ShoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            if (cart == null)
                return null;
            var package = await _packageRepository.GetPackageByIdAsync(packageInCart.PackageId);
            if (package == null)
                return null;
             var newPackageInCart = new PackageInCartModel()
            {
                PackageId = packageInCart.PackageId,
               
                CartId = cart.Id

            };

            var createPackageInCart = await _packageInCartRepository.CreatePackageInCartAsync(newPackageInCart);
            var createPackageWithDetails= await _packageInCartRepository.GetPackageInCartByIdAsync(createPackageInCart.Id);
            if(createPackageWithDetails==null)
               return null; 
          
                cart.SumPrice += package.Price;
                await _ShoppingCartRepository.UpdateShoppingCartAsync(cart);
            
            return new PackageInCartDto
            {
                Id = createPackageWithDetails.Id,
                PackageId = createPackageWithDetails.PackageId,
                PackageName = createPackageWithDetails.Package?.Name,
                PackagePrice = createPackageWithDetails.Package?.Price ?? 0,
                GiftsInPackage = createPackageWithDetails.GiftsInPackage?.Select(g => new GiftInCartDto
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
            var userIdCalm = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdCalm, out int userId))
            {
                return false;
            }
            var cart = await _ShoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
            if (cart == null)
                return false;
            if(cart.Id!= packageInCart.CartId) 
                return false;
            var price=packageInCart?.Package?.Price??0;
          
            
                cart.SumPrice -= price;
                await _ShoppingCartRepository.UpdateShoppingCartAsync(cart);
            

            return await _packageInCartRepository.DeletePackageInCartAsync(id);
          

        }
    }
}

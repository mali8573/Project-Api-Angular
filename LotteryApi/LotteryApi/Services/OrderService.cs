//using LotteryApi.Dtos;
//using LotteryApi.Models;
//using LotteryApi.Repositories;

//namespace LotteryApi.Services
//{
//    public class OrderService
//    {
//        private readonly OrderRepository _orderRepository = new();


//        public async Task<ShoppingCartDto?> GetOrderByIdAsync(int id)
//        {
//            var order = await _orderRepository.GetOrderByIdAsync(id);

//            return order != null ? new ShoppingCartDto
//            {
//                Id = shoppingCart.Id,
//                ParticipantId = shoppingCart.ParticipantId,
//                ParticipantName = shoppingCart.Participant?.Name,
//                PackagesInShoppingCart = shoppingCart.PackagesInShoppingCart?.Select(p => new PackageInCartDto
//                {
//                    Id = p.Id,
//                    PackageId = p.PackageId,
//                    PackageName = p.Package?.Name,
//                    PackagePrice = p.Package?.Price ?? 0,
//                    GiftsInPackage = p.GiftsInPackage?.Select(g => new GiftInCartDto
//                    {
//                        Id = g.Id,
//                        GiftId = g.GiftId,
//                        GiftName = g.Gift?.Name,
//                        giftPictureUrl = g.Gift?.PictureUrl,
//                        giftCardPrice = g.Gift?.CardPrice.ToString(),
//                        Qty = g.Qty
//                    }).ToList() ?? []
//                }).ToList() ?? [],
//                SumPrice = shoppingCart.SumPrice
//            } : null;
//        }

//        public async Task<ShoppingCartDto> CreateShoppingCartAsync(ShoppingCartCreateDto shoppingcart)
//        {
//            var newShoppingCart = new ShoppingCartModel()
//            {

//                ParticipantId = shoppingcart.ParticipantId,

//            };

//            var createShoppingCart = await _shoppingCartRepository.CreateShoppingCartAsync(newShoppingCart);
//            return createShoppingCart != null ? new ShoppingCartDto
//            {
//                Id = createShoppingCart.Id,
//                ParticipantId = createShoppingCart.ParticipantId,
//                ParticipantName = createShoppingCart.Participant?.Name,
//                PackagesInShoppingCart = [],
//                SumPrice = 0

//            } : null;
//        }
//    }
//}

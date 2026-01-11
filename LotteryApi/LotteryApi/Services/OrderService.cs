using LotteryApi.Dtos;
using LotteryApi.Models;
using LotteryApi.Repositories;

namespace LotteryApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public OrderService(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return orders.Select(o => new OrderDto
            {

                Id = o.Id,
                ParticipantId = o.ParticipantId,
                ParticipantName = o.Participant?.Name,
                SumPrice = o.SumPrice,
                date = o.date,
                PackagesInOrder = o.PackagesInOrder?.Select(p => new PackageInOrderDto
                {
                    PackageId = p.PackageId,
                    PackageName = p.Package?.Name,
                    PriceAtPurchase = p.PriceAtPurchase,

                    GiftsInPackage = p.GiftsInPackage?.Select(g => new GiftInOrderDto
                    {
                        Id = g.Id,
                        GiftId = g.GiftId,
                        GiftName = g.Gift?.Name,
                        GiftPictureUrl = g.Gift?.PictureUrl,
                        GiftCardPrice = g.Gift?.CardPrice.ToString(),
                        IsWinner = g.IsWinner
                    }).ToList() ?? new List<GiftInOrderDto>()
                }).ToList() ?? new List<PackageInOrderDto>()
            }).ToList();
        }




        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            return order != null ? new OrderDto
            {
                Id = order.Id,
                ParticipantId = order.ParticipantId,
                ParticipantName = order.Participant?.Name,
                SumPrice = order.SumPrice,
                date = order.date,
                PackagesInOrder = order.PackagesInOrder?.Select(p => new PackageInOrderDto
                {
                    PackageId = p.PackageId,
                    PackageName = p.Package?.Name,
                    PriceAtPurchase = p.PriceAtPurchase,

                    GiftsInPackage = p.GiftsInPackage?.Select(g => new GiftInOrderDto
                    {
                        Id = g.Id,
                        GiftId = g.GiftId,
                        GiftName = g.Gift?.Name,
                        GiftPictureUrl = g.Gift?.PictureUrl,
                        GiftCardPrice = g.Gift?.CardPrice.ToString(),
                        IsWinner = g.IsWinner
                    }).ToList() ?? new List<GiftInOrderDto>()
                }).ToList() ?? new List<PackageInOrderDto>()
            } : null;

        }

        public async Task<OrderDto> CreateShoppingCartAsync(ShoppingCartDto shoppingcart)
        {
            var newOrder = new OrderModel()
            {

                ParticipantId = shoppingcart.ParticipantId,
                SumPrice = shoppingcart.SumPrice,
                date = DateOnly.FromDateTime(DateTime.UtcNow),
                PackagesInOrder = shoppingcart.PackagesInShoppingCart.Select(p => new PackageInOrderModel
                {
                    PackageId = p.PackageId,

                    PriceAtPurchase = p.PackagePrice,

                    GiftsInPackage = p.GiftsInPackage.SelectMany(g =>
                    Enumerable.Range(0, g.Qty).Select(_ => new GiftInOrderModel
                    {
                        GiftId = g.GiftId,
                        IsWinner = false
                    })).ToList()
                }).ToList()

            };

            var createOrder = await _orderRepository.CreateOrderAsync(newOrder);
            await _shoppingCartRepository.EmptyCartAsync(shoppingcart.Id);
            return await GetOrderByIdAsync(createOrder.Id);
        }
    }
}

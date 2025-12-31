using LotteryApi.Dtos;
using LotteryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotteryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftInOrderController : ControllerBase
    {
        private readonly GiftInOrderService _giftInOrderService = new();


        [HttpGet("{id}")]
        public async Task<ActionResult<GiftInOrderDto>> GetGiftInOrderByIdAsync(int id)
        {
            var giftInOrder = await _giftInOrderService.GetGiftInOrderByIdAsync(id);
            if (giftInOrder == null)
            {
                return NotFound(new { message = $"GiftInOrder with ID {id} not found." });
            }
            return Ok(giftInOrder);
        }
    }
}

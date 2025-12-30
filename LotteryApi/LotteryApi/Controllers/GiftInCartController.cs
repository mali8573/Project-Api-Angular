using LotteryApi.Dtos;
using LotteryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotteryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftInCartController : ControllerBase
    {
        private readonly GiftInCartService _giftInCartService = new();


        [HttpGet("{id}")]
        public async Task<ActionResult<GiftInCartDto>> GetGiftInCartByIdAsync(int id)
        {
            var giftInCart = await _giftInCartService.GetGiftInCartByIdAsync(id);
            if (giftInCart == null)
            {
                return NotFound(new { message = $"GiftInCart with ID {id} not found." });
            }
            return Ok(giftInCart);
        }
        [HttpGet("{id,giftId}")]
        public async Task<ActionResult<GiftInCartDto>> GetGiftInCartByIdAndByPackageAsync(int id, int giftId)
        {
            var giftInCart = await _giftInCartService.GetGiftInCartByIdAndByPackageAsync(id, giftId);
            if (giftInCart == null)
            {
                return NotFound(new { message = $"GiftInCart with Cart ID {id} and Gift ID {giftId} not found." });
            }
            return Ok(giftInCart);
        }
        [HttpPost]
        public async Task<ActionResult<GiftInCartDto>> CreateOrUpdateGiftInCartAsync([FromBody] GiftInCartCreateDto giftInCart)
        {
            var newGiftInCart = await _giftInCartService.CreateOrUpdateGiftInCartAsync(giftInCart);
            return Ok(newGiftInCart);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePackageInCartAsync(int id)
        {
            var isDeleted = await _giftInCartService.DeleteGiftInCartAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = $"GiftInCart with ID {id} not found." });
            }
            return NoContent();
        }
    }
}

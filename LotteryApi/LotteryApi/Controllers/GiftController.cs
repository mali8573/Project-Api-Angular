using LotteryApi.Dtos;
using LotteryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotteryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly GiftService _giftService = new();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDto>>> GetGiftAsync()
        {
            var gifts = await _giftService.GetGiftsAsync();
            return Ok(gifts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDto>> GetGiftByIdAsync(int id)
        {
            var gift = await _giftService.GetGiftByIdAsync(id);
            if (gift == null)
            {
                return NotFound(new { message = $"Gift with ID {id} not found." });
            }
            return Ok(gift);
        }
        [HttpPost]
        public async Task<ActionResult<GiftDto>> CreateGiftAsync([FromBody] GiftCreateDto gift)
        {
            var newGift = await _giftService.CreateGiftAsync(gift);
            return Ok(newGift);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GiftDto>> UpdatePackageAsync(int id, [FromBody] GiftUpdateDto gift)
        {
            var updateGift = await _giftService.UpdateGiftAsync(id, gift);
            if (updateGift == null)
            {
                return NotFound(new { message = $"Gift with ID {id} not found." });
            }
            return Ok(updateGift);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGiftAsync(int id)
        {
            var isDeleted = await _giftService.DeleteGiftAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = $"Gift with ID {id} not found." });
            }
            return NoContent();
        }
    }
}

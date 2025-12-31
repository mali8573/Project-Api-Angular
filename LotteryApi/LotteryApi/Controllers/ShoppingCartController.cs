using LotteryApi.Dtos;
using LotteryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotteryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartService _shoppingCartService = new();

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartByIdAsync(int id)
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCartByIdAsync(id);
            if (shoppingCart == null)
            {
                return NotFound(new { message = $"ShoppingCart with ID {id} not found." });
            }
            return Ok(shoppingCart);
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> CreatePackageAsync([FromBody] ShoppingCartCreateDto shoppingCart)
        {
            var newShoppingCart = await _shoppingCartService.CreateShoppingCartAsync(shoppingCart);
            return Ok(newShoppingCart);
        }
   
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePackageAsync(int id)
        {
            var isDeleted = await _shoppingCartService.DeleteShoppingCartAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = $"ShoppingCart with ID {id} not found." });
            }
            return NoContent();
        }
        [HttpDelete("clear/{id}")]

        public async Task<ActionResult> EmptyCartAsync(int id)
        {
            var isEmpty = await _shoppingCartService.EmptyCartAsync(id);
            if (!isEmpty)
            {
                return NotFound(new { message = $"ShoppingCart with ID {id} not found." });
            }
            return NoContent();
        }

    }
}

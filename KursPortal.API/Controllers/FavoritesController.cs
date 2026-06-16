using KursPortal.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var favorite = await _favoriteService.GetUserFavoriteAsync(userId);
            return Ok(favorite);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Guid userId, Guid courseId)
        {
            var result = await _favoriteService.AddToFavoriteAsync(userId, courseId);
            if (result.Contains("already"))
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Clear(Guid userId)
        {
            await _favoriteService.ClearFavoriteAsync(userId);
            return Ok();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(Guid userId, Guid courseId)
        {
            var result = await _favoriteService.RemoveFromFavoriteAsync(userId, courseId);
            if (result == "Favorite not found" || result == "Favorite not found in cart")
                return NotFound(result);

            return Ok(result);
        }
    }
}

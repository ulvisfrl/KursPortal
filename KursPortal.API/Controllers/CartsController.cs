using KursPortal.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var cart = await _cartService.GetUserCartAsync(userId);
        return Ok(cart);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromQuery] Guid userId, [FromQuery] Guid courseId)
    {
        var result = await _cartService.AddToCartAsync(userId, courseId);

        if (result.Contains("already"))
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Clear(Guid userId)
    {
        await _cartService.ClearCartAsync(userId);
        return Ok();
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFromCart(Guid userId, Guid courseId)
    {
        var result = await _cartService.RemoveFromCartAsync(userId, courseId);

        if (result == "Cart not found" || result == "Course not found in cart")
            return NotFound(result);

        return Ok(result);
    }
}
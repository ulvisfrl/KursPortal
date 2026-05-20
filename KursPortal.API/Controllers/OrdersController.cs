using KursPortal.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromQuery] Guid userId)
        {
            try
            {
                await _orderService.CheckoutAsync(userId);

                return Ok(new
                {
                    Message = "Checkout successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("user-orders/{userId}")]
        public async Task<IActionResult> GetUserOrders(Guid userId)
        {
            var orders = await _orderService
                .GetUserOrdersAsync(userId);

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService
                .GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return NotFound(new
                {
                    Message = "Order not found"
                });
            }

            return Ok(order);
        }
    }
}
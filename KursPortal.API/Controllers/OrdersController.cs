using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        readonly IUserCourseRepository _userCourseRepository;

        public OrdersController(IOrderService orderService, IUserCourseRepository userCourseRepository)
        {
            _orderService = orderService;
            _userCourseRepository = userCourseRepository;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromQuery] Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                    return BadRequest("UserId is empty");

                var url = await _orderService.CheckoutAsync(userId);
                return Ok(new { checkoutUrl = url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user-orders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userClaim = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userClaim)) return Unauthorized();

            var userId = Guid.Parse(userClaim);
            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment([FromQuery] Guid orderId)
        {
            if (orderId == Guid.Empty)
                return BadRequest("OrderId is empty");

            try
            {
                var result = await _orderService.ConfirmPaymentAsync(orderId);
                if (!result) return NotFound("Order not found");

                return Ok(new { message = "Payment confirmed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
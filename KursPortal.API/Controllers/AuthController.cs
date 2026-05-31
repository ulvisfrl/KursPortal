using KursPortal.Business.Abstract;
using KursPortal.DTOs.DTOs.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KursPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] string email)
        {
            await _authService.PasswordResetAsync(email);
            return Ok();
        }


        //[HttpPost("update-password")]
        //public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        //{
        //    await _authService.UpdatePasswordAsync(dto.UserId, dto.ResetToken, dto.Password);
        //    return Ok();
        //}
    }
}

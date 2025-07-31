using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunEduProject.Model.DTos;
using SunEduProject.Service.IServiceRespository;

namespace SunEduProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceRespository authServiceRespository;

        public AuthController(IAuthServiceRespository authServiceRespository)
        {
            this.authServiceRespository = authServiceRespository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await authServiceRespository.RegisterAsync(registerDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await authServiceRespository.LoginAsync(loginDto);

                // Set JWT token in secure HttpOnly cookie
                Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
        {
            try
            {
                var result = await authServiceRespository.ForgetPasswordAsync(forgetPasswordDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await authServiceRespository.ResetPasswordAsync(resetPasswordDto);
                if (result)
                {
                    return Ok(new { Message = "Password reset successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to reset password." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Clear the JWT token cookie
            Response.Cookies.Delete("X-Access-Token");
            return Ok(new { Message = "Logged out successfully." });


        }

    }
}

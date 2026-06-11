using Microsoft.AspNetCore.Mvc;
using zenvy.application.DTOs.Auth;
using zenvy.Application.Auth;

namespace zenvy.api.Controller;
[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await authService.LoginAsync(request);
        if (response == null)
        {
            return Unauthorized(new { Message = "Invalid email or password" });
        }
        return Ok(response);
    }
}
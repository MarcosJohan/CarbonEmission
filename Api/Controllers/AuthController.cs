using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpGet("login")]
    [AllowAnonymous]
    public ActionResult<object> Login([FromHeader] string username, [FromHeader] string password)
    {
        return authService.GetToken(username, password);
    }
}
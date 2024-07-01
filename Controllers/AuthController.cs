using ARS.Models;
using ARS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Controllers;

[Route("api")]
public class AuthController(AuthService authService) : Controller {
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UserLoginDto userLogin) {
        var userResult = authService.Authenticate(userLogin);
        if (userResult.Failure)
            return Unauthorized(userResult.Message);
        return Ok(new { token = authService.GenerateToken(userLogin.Username) });
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("token-login")]
    public IActionResult LoginByToken([FromBody] string token) {
        var userResult = authService.Authenticate(token);
        if (userResult.Failure)
            return Unauthorized(userResult.Message);
        return Ok(new { name = userResult.Value!.Name });
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] UserRegisterDto userRegister) {
        var userResult = authService.Register(userRegister);
        if (userResult.Failure)
            return BadRequest(userResult.Message);
        return Ok(new { token = authService.GenerateToken(userResult.Value!.Username) });
    }
}
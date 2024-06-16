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
        var user = authService.Authenticate(userLogin);
        if (user is null)
            return Unauthorized();
        return Ok(new { token = authService.GenerateToken(userLogin) });
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("token-login")]
    public IActionResult LoginByToken([FromBody] string token) {
        var user = authService.Authenticate(token);
        if (user is null)
            return Unauthorized();
        return Ok(new { user.Id, user.Name });
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] UserRegisterDto userRegister) {
        return Ok("Id: " + authService.Register(userRegister).Id);
    }
}
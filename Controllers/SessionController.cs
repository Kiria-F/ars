using ARS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Controllers;

[Route("api")]
[AllowAnonymous]
public class SessionController(
    AuthService authService,
    SessionService sessionService) : Controller {
    [HttpPost]
    [Route("close-session")]
    public ActionResult CloseSession([FromBody] string token) {
        var userResult = authService.Authenticate(token);
        if (userResult.Success && sessionService.CloseSession(userResult.Value!.Id)) {
            return Ok("Session closed");
        }
        return Unauthorized("Session not found");
    }

    [HttpGet]
    [Route("active-sessions")]
    public IEnumerable<string> GetAllActive() {
        return sessionService.GetActiveUsers().Select(u => u.Username);
    }
}
using ARS.Services;
using ARS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Controllers;

public class GreetingsController : Controller
{
    private readonly IGreetingsService _greetingsService;

    public GreetingsController(IGreetingsService greetingsService)
    {
        _greetingsService = greetingsService;
    }

    [Route("hi")]
    public string Hi()
    {
        return _greetingsService.Greetings();
    }
}
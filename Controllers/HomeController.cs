using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ARS.Models;
using ARS.Services.Interfaces;

namespace ARS.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRandomGeneratorService _randomGeneratorService;

        public HomeController(
            ILogger<HomeController> logger,
            IRandomGeneratorService randomGeneratorService)
        {
            _logger = logger;
            _randomGeneratorService = randomGeneratorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
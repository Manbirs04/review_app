using Microsoft.AspNetCore.Mvc;
using ReviewHubBackend.Models;
using System.Diagnostics;

namespace ReviewHubBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Content("Welcome to ReviewHub API! Use API endpoints to interact.");
        }

        public IActionResult Privacy()
        {
            return Content("This is the privacy policy page.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

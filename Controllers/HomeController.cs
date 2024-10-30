using Microsoft.AspNetCore.Mvc;
using MordexIntegration.Models;
using System.Diagnostics;

namespace MordexIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public IActionResult CreateTokens(TokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Mordex", model);
            }
            // In a real application, you would call the API here
            return RedirectToAction("Mordex");
        }

        [HttpPost]
        public IActionResult TransferTokens(TokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Mordex", model);
            }
            // In a real application, you would call the API here
            return RedirectToAction("Mordex");
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

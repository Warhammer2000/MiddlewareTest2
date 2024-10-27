using JustTest.Middlewaresa;
using JustTest.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;

namespace JustTest.Controllers
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
            return View();
        }
        public IActionResult ErrorThrow()
        {
            throw new Exception("This is Test Exception");
        }

        public static void GetRef(MiddlewareBase type)
        {
            Log.Logger.Information($"Выбран middleware с ID{type.id}");
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

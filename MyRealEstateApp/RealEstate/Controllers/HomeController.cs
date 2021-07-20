namespace RealEstate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private protected readonly ILogger<HomeController> Logger;

        public HomeController(ILogger<HomeController> logger)
            => Logger = logger;

        public IActionResult Index() //<= show show last 3 added properties in carousel
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

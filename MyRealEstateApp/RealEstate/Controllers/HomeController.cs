using System.Threading.Tasks;
using RealEstate.Models.Home;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Diagnostics;
    using static Common.GlobalConstants;

    public class HomeController : Controller
    {
        private readonly IHomeService Service;

        public HomeController(IHomeService service)
        {
            this.Service = service;
        }

        public async Task<IActionResult> Index()
        {
            NewestEstatesViewModel estates = new NewestEstatesViewModel();

            estates.AddEstates(await this.Service.GetLastAddedEstatesAsync(DefaultLastEstatesCount));

            return View(estates);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

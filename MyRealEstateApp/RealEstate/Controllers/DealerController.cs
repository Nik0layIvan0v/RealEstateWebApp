using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    [Authorize]
    public class DealerController : Controller
    {
        private readonly IDealerService Service;

        public DealerController(IDealerService service)
        {
            Service = service;
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}

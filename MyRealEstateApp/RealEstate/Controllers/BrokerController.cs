using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    [Authorize]
    public class BrokerController : Controller
    {
        private readonly IBrokerService Service;

        public BrokerController(IBrokerService service)
        {
            Service = service;
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Infrastructure;
using RealEstate.Models;
using RealEstate.Models.Brokers;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    [Authorize]
    public class BrokersController : Controller
    {
        private readonly IBrokerService Service;

        public BrokersController(IBrokerService service)
        {
            Service = service;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(BecomeBrokerFormModel model)
        {
            string userId = this.User.GetLoggedInUserId();

            bool userIsAlreadyBroker = await this.Service.IsUserAlreadyBroker(userId);

            if (userIsAlreadyBroker)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return await Create(model);
            }

            Broker broker = new Broker
            {
                Name = model.BrokerName,
                PhoneNumber = model.PhoneNumber,
                UserId = userId,
            };

            await this.Service.AddBroker(broker);

            return this.RedirectToAction(nameof(EstatesController.Create), "Estates");
        }
    }
}

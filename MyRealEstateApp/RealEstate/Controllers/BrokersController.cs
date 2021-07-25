using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Infrastructure;
using RealEstate.Models;
using RealEstate.Models.Brokers;
using RealEstate.Services;
using RealEstate.Services.Models;

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

        public IActionResult CreateBroker()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBroker(BecomeBrokerFormModel model)
        {
            string userId = this.User.GetLoggedInUserId();

            bool userIsAlreadyBroker = await this.Service.IsUserAlreadyBrokerAsync(userId);

            if (userIsAlreadyBroker)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return await CreateBroker(model);
            }

            Broker broker = new Broker
            {
                Name = model.BrokerName,
                PhoneNumber = model.PhoneNumber,
                UserId = userId,
            };

            await this.Service.CreateBrokerAsync(broker);

            return this.RedirectToAction(nameof(EstatesController.Create), "Estates");
        }

        public async Task<IActionResult> MyEstateOffers()
        {
            string userId = this.User.GetLoggedInUserId();

            IEnumerable<MyEstateServiceModel> myEstates = await this.Service.GetBrokerEstatesAsync(userId);

            return this.View(myEstates);
        }
    }
}

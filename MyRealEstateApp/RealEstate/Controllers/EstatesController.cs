using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Infrastructure;
using RealEstate.Models.Estates;
using RealEstate.Services;
using RealEstate.Services.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Controllers
{

    [Authorize]
    public class EstatesController : Controller
    {
        private readonly IEstateService EstateService;
        private readonly IBrokerService BrokerService;

        public EstatesController(IEstateService estateService, IBrokerService brokerService)
        {
            this.EstateService = estateService;
            this.BrokerService = brokerService;
        }

        public async Task<IActionResult> Create()
        {
            string userId = User.GetLoggedInUserId();

            if (await this.BrokerService.IsUserAlreadyBrokerAsync(userId) == false)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            var dropdownData =  await this.EstateService.GetDropDownDataAsync();

            AddEstateInputModel model = new AddEstateInputModel(); ;

            model.EstateTypeViewModels = dropdownData.EstateTypeModels;
            model.CurrencyViewModels = dropdownData.CurrencyModels;
            model.AreasViewModels = dropdownData.Areas;
            model.TypeOfDeals = dropdownData.TradeTypeModels;
            model.FutureModels = dropdownData.FutureModels.ToList();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddEstateInputModel model)
        {
            string loggedUserId = User.GetLoggedInUserId();

            if (await this.BrokerService.IsUserAlreadyBrokerAsync(loggedUserId) == false)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            int brokerId = await this.BrokerService.GetBrokerIdAsync(loggedUserId);

            model.FutureModels.RemoveAll(x => x.IsChecked == false);

            if (!ModelState.IsValid)
            {
                var dropdownData = await this.EstateService.GetDropDownDataAsync();
                model.EstateTypeViewModels = dropdownData.EstateTypeModels;
                model.CurrencyViewModels = dropdownData.CurrencyModels;
                model.AreasViewModels = dropdownData.Areas;
                model.TypeOfDeals = dropdownData.TradeTypeModels;
                model.FutureModels = dropdownData.FutureModels.ToList();

                return await this.Create();
            };

            EstateModel estate = new EstateModel
            {
                Squaring = model.Squaring,
                Floor = model.Floor,
                Price = model.Price,
                CurrencyId = model.CurrencyId,
                EstateTypeId = model.EstateTypeId,
                AreaId = model.AreaId,
                CityId = model.CityId,
                NeighborhoodId = model.NeighborhoodId,
                Description = model.Description,
                TypeOfTradeId = model.TypeOfTradeId,
                SelectedFutures = model.FutureModels,
                Images = new List<byte[]>(),
                BrokerId = brokerId,
            };

            if (model.ImageFiles != null)
            {
                foreach (var imageFile in model.ImageFiles)
                {
                    await using Stream temp = imageFile.OpenReadStream();

                    await using MemoryStream memoryStream = new MemoryStream();

                    await temp.CopyToAsync(memoryStream);

                    byte[] readedBytes = memoryStream.ToArray();

                    estate.Images.Add(readedBytes);
                }
            }

            string estateId = await this.EstateService.CreateEstateAsync(estate);

            return this.RedirectToAction(nameof(Details),"Estates",new {Id = estateId});
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(AllEstateQueryModel queryEstateModel)
        {
            queryEstateModel.EstateListingViewModels = await EstateService.GetAllEstatesAsync(queryEstateModel.CurrentPage, queryEstateModel.EstatesPerPage);

            queryEstateModel.TotalEstates = await this.EstateService.GetCountOfAllEstatesAsync();

            return this.View(queryEstateModel);
        }

        public async Task<ActionResult<EstateDetailsModel>> Details(string id)
        {
            EstateDetailsModel estateInfo = await this.EstateService.GetEstateDetailsAsync(id);

            return this.View(estateInfo);
        }

        public async Task<IActionResult> Edit(string id)
        {
            string loggedUserId = User.GetLoggedInUserId();

            if (await this.BrokerService.IsUserAlreadyBrokerAsync(loggedUserId) == false)
            {
                //OR USER DON'T OWN ESTATE OFFER!
            }

            return this.Ok();
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return this.RedirectToAction(nameof(Details),"Estates");
        }

        [HttpPost]
        public IActionResult Delete(string estateId)
        {
            return this.Ok();
        }
    }
}

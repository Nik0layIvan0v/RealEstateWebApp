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

        public EstatesController(IEstateService estateService)
        {
            this.EstateService = estateService;
        }

        public async Task<IActionResult> Create()
        {
            if (await this.IsCurrentLoggedUserIsBrokerAsync() == false)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            var dropdownData = this.EstateService.GetDropDownData();

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
            //if (await this.IsCurrentLoggedUserIsBrokerAsync() == false)
            //{
            //    return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            //}

            int brokerId = await this.EstateService.GetBrokerIdAsync(this.User.GetLoggedInUserId());

            model.FutureModels.RemoveAll(x => x.IsChecked == false);

            if (!ModelState.IsValid)
            {
                var dropdownData = this.EstateService.GetDropDownData();
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
                    await using var temp = imageFile.OpenReadStream();

                    await using var ms = new MemoryStream();

                    await temp.CopyToAsync(ms);

                    byte[] readedBytes = ms.ToArray();

                    estate.Images.Add(readedBytes);
                }
            }

            string estateId = await this.EstateService.CreateEstateAsync(estate);

            return this.Redirect($"/Estate/Details?id={estateId}");
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
            if (await this.IsCurrentLoggedUserIsBrokerAsync() == false)
            {
                //OR USER DON'T OWN ESTATE OFFER!
            }

            return this.Ok();
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return this.Redirect("Estate/Details");
        }

        [HttpPost]
        public IActionResult Delete(string estateId)
        {
            return this.Ok();
        }

        private async Task<bool> IsCurrentLoggedUserIsBrokerAsync()
        {
            string userId = User.GetLoggedInUserId();

            return await this.EstateService.IsUserIsBrokerAsync(userId);
        }
    }
}

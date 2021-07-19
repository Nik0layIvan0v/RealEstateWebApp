using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.Estates;
using RealEstate.Services;
using RealEstate.Services.Models;

namespace RealEstate.Controllers
{
    public class EstateController : Controller
    {
        private readonly IEstateService EstateService;

        public EstateController(IEstateService estateService)
        {
            this.EstateService = estateService;
        }

        public IActionResult Create()
        {
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
        public IActionResult Create(AddEstateInputModel model)
        {
            model.FutureModels.RemoveAll(x => x.IsChecked == false);

            if (!ModelState.IsValid)
            {
                var dropdownData = this.EstateService.GetDropDownData();
                model.EstateTypeViewModels = dropdownData.EstateTypeModels;
                model.CurrencyViewModels = dropdownData.CurrencyModels;
                model.AreasViewModels = dropdownData.Areas;
                model.TypeOfDeals = dropdownData.TradeTypeModels;
                model.FutureModels = dropdownData.FutureModels.ToList();

                return this.Create();
            };


            string estateId = this.EstateService.CreateEstate(new EstateModel
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
                SelectedFutures = model.FutureModels
            });

            return this.Redirect($"/Estate/Details?id={estateId}");
        }
        public async Task<IActionResult> All([FromQuery] AllEstateQueryModel queryEstateModel)
        {
            queryEstateModel.EstateListingViewModels = await EstateService.GetAllEstatesAsync(queryEstateModel.CurrentPage, queryEstateModel.EstatesPerPage);

            queryEstateModel.TotalEstates = await this.EstateService.GetCountOfAllEstatesAsync();

            return this.View(queryEstateModel);
        }

        public IActionResult Details(string id)
        {
            return this.View();
        }

        public IActionResult Edit(string id)
        {
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

    }
}

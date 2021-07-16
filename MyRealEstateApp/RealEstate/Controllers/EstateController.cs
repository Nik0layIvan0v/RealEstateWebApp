using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.Estates;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    public class EstateController : Controller
    {
        private readonly IEstateService EstateService;

        public EstateController(IEstateService estateService) //<== service
        {
            this.EstateService = estateService;
        }

        public IActionResult Create(CreateEstateViewModel model)
        {
            var dropdownData = this.EstateService.GetDropDownData();

            model.EstateTypes = dropdownData.EstateTypeViewModels;
            model.Currency = dropdownData.CurrencyViewModels;
            model.Areas = dropdownData.Areas;

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(string model) //<= model from form!
        {
            return this.Redirect("/"); //<= redirect to my offers!
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
        public IActionResult Edit() //<= model!
        {
            return this.Redirect("Estate/Details");
        }

        [HttpPost]
        public IActionResult Delete(string estateId)
        {
            return this.Ok();
        }

        public IActionResult All()
        {
            return this.Ok();
        }
    }
}

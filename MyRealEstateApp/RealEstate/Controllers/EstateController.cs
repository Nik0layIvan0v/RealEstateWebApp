using System;
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

        public IActionResult Create()
        {
            var dropdownData = this.EstateService.GetDropDownData();

            AddEstateInputModel model = new AddEstateInputModel();;

            model.EstateTypeViewModels = dropdownData.EstateTypeViewModels;
            model.CurrencyViewModels = dropdownData.CurrencyViewModels;
            model.AreasViewModels = dropdownData.Areas;

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(AddEstateInputModel model) //<= model from form!
        {
            if (!ModelState.IsValid)
            {
                var dropdownData = this.EstateService.GetDropDownData();
                model.EstateTypeViewModels = dropdownData.EstateTypeViewModels;
                model.CurrencyViewModels = dropdownData.CurrencyViewModels;
                model.AreasViewModels = dropdownData.Areas;

                return this.Create();
            };

            return this.Redirect("/");
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

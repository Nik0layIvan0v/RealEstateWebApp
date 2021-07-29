using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly ICommentService CommentService;

        public EstatesController(IEstateService estateService, IBrokerService brokerService, ICommentService commentService)
        {
            this.EstateService = estateService;
            this.BrokerService = brokerService;
            this.CommentService = commentService;
        }

        public async Task<IActionResult> Create()
        {
            string userId = User.GetLoggedInUserId();

            if (await this.BrokerService.IsUserAlreadyBrokerAsync(userId) == false)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            EstateFormModel model = await this.LoadEstateFormModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstateFormModel model)
        {
            string loggedUserId = User.GetLoggedInUserId();

            if (await this.BrokerService.IsUserAlreadyBrokerAsync(loggedUserId) == false)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            int brokerId = await this.BrokerService.GetBrokerIdAsync(loggedUserId);

            model.FutureModels.RemoveAll(x => x.IsChecked == false); //Removes all unchecked boxes

            if (!ModelState.IsValid)
            {
                model = await this.LoadEstateFormModel();

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

            return this.RedirectToAction(nameof(Details), "Estates", new { Id = estateId });
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

            var estateComments = await this.CommentService.GetCommentsByEstateId(id);

            foreach (var comment in estateComments)
            {
                estateInfo.Comments.Add(comment);
            }

            return this.View(estateInfo);
        }

        public async Task<IActionResult> Edit(string id)
        {
            string userId = this.User.GetLoggedInUserId();

            if (await this.BrokerService.IsUserAlreadyBrokerAsync(userId) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            EstateModel estateDb = await this.EstateService.GetEstateFormModelById(id);

            if (estateDb.BrokerId != await BrokerService.GetBrokerIdAsync(userId))
            {
                return Unauthorized();
            }

            EstateFormModel formModel = await this.LoadEstateFormModel(estateDb);

            return this.View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EstateFormModel formModel)
        {
            int brokerId = await BrokerService.GetBrokerIdAsync(this.User.GetLoggedInUserId());

            formModel.FutureModels.RemoveAll(x => x.IsChecked == false); //Removes all unchecked boxes

            EstateModel estateModel = new EstateModel
            {
                BrokerId = brokerId,
                Squaring = formModel.Squaring,
                Floor = formModel.Floor,
                Price = formModel.Price,
                CurrencyId = formModel.CurrencyId,
                EstateTypeId = formModel.EstateTypeId,
                AreaId = formModel.AreaId,
                CityId = formModel.CityId,
                NeighborhoodId = formModel.NeighborhoodId,
                Description = formModel.Description,
                TypeOfTradeId = formModel.TypeOfTradeId,
                SelectedFutures = formModel.FutureModels,
                Images = new List<byte[]>()
            };

            if (formModel.ImageFiles != null)
            {
                foreach (var imageFile in formModel.ImageFiles)
                {
                    await using Stream temp = imageFile.OpenReadStream();

                    await using MemoryStream memoryStream = new MemoryStream();

                    await temp.CopyToAsync(memoryStream);

                    byte[] readedBytes = memoryStream.ToArray();

                    estateModel.Images.Add(readedBytes);
                }
            }

            bool isEdited = await this.EstateService.EditEstateAsync(id, estateModel);

            if (!isEdited)
            {
                return BadRequest();
            }

            return this.RedirectToAction(nameof(BrokersController.MyEstateOffers), "Brokers");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            return this.Ok();
        }

        private async Task<EstateFormModel> LoadEstateFormModel(EstateModel estateModel = null)
        {
            var dropdownData = await this.EstateService.GetDropDownDataAsync();

            EstateFormModel model = new EstateFormModel
            {
                EstateTypeViewModels = dropdownData.EstateTypeModels,
                CurrencyViewModels = dropdownData.CurrencyModels,
                AreasViewModels = dropdownData.Areas,
                TypeOfDeals = dropdownData.TradeTypeModels,
                FutureModels = dropdownData.FutureModels.ToList(),
            };

            if (estateModel != null)
            {
                foreach (var feture in dropdownData.FutureModels)
                {
                    var populatedCheckbox = estateModel.SelectedFutures.FirstOrDefault(x => x.Id == feture.Id);

                    if (populatedCheckbox != null)
                    {
                        feture.IsChecked = populatedCheckbox.IsChecked;
                    }
                }

                model.Squaring = estateModel.Squaring;
                model.Floor = estateModel.Floor;
                model.Price = estateModel.Price;
                model.CurrencyId = estateModel.CurrencyId;
                model.EstateTypeId = estateModel.EstateTypeId;
                model.AreaId = estateModel.AreaId;
                model.CityId = estateModel.CityId;
                model.NeighborhoodId = estateModel.NeighborhoodId;
                model.Description = estateModel.Description;
                model.TypeOfTradeId = estateModel.TypeOfTradeId;
                model.ImageFiles = new List<IFormFile>();
            }

            return model;
        }
    }
}
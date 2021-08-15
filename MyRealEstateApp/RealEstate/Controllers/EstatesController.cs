using AutoMapper;
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
using static RealEstate.Common.GlobalConstants.GloblalMessages;

namespace RealEstate.Controllers
{

    [Authorize]
    public class EstatesController : Controller
    {
        private readonly IEstateService EstateService;
        private readonly IBrokerService BrokerService;
        private readonly ICommentService CommentService;
        private readonly IMapper AutoMapper;

        public EstatesController(
            IEstateService estateService,
            IBrokerService brokerService,
            ICommentService commentService,
            IMapper mapper)
        {
            this.EstateService = estateService;
            this.BrokerService = brokerService;
            this.CommentService = commentService;
            this.AutoMapper = mapper;
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

            int brokerId = await this.BrokerService.GetBrokerIdAsync(loggedUserId);

            if (brokerId == 0)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            model.SelectedFutures.RemoveAll(x => x.IsChecked == false); //Removes all unchecked boxes

            if (!ModelState.IsValid)
            {
                model = await this.LoadEstateFormModel();

                return await this.Create();
            };

            EstateServiceModel estate = this.AutoMapper.Map<EstateServiceModel>(model);

            estate.BrokerId = brokerId;

            estate.Images = await this.GetEstateImages(model.ImageFiles);

            string estateId = await this.EstateService.CreateEstateAsync(estate);

            this.TempData[GlobalTempMessageKey] = SuccsesfullEstateCreation;

            return this.RedirectToAction(nameof(Details), "Estates", new { Id = estateId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(AllEstateQueryModel queryEstateModel)
        {
            queryEstateModel.EstateListingViewModels = await EstateService
                .GetAllEstatesAsync(queryEstateModel.CurrentPage,
                                    queryEstateModel.EstatesPerPage,
                                    queryEstateModel.SearchTerm);

            queryEstateModel.TotalEstates = await this.EstateService.GetCountOfAllEstatesAsync();

            return this.View(new AllEstateQueryModel
            {
                SearchTerm = queryEstateModel.SearchTerm,
                EstateListingViewModels = queryEstateModel.EstateListingViewModels
            });
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

            EstateServiceModel estateDb = await this.EstateService.GetEstateFormModelById(id);

            if (estateDb.BrokerId != await BrokerService.GetBrokerIdAsync(userId) && User.IsAdmin() == false)
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

            if (brokerId == 0 && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(BrokersController.CreateBroker), "Brokers");
            }

            int estateBrokerId = await this.EstateService.GetEstateBrokerId(id);

            if (brokerId != estateBrokerId && !User.IsAdmin())
            {
                return this.Unauthorized();
            }

            formModel.SelectedFutures.RemoveAll(x => x.IsChecked == false); //Removes all unchecked boxes

            EstateServiceModel estateModel = this.AutoMapper.Map<EstateServiceModel>(formModel);

            estateModel.BrokerId = brokerId;

            estateModel.Images = await this.GetEstateImages(formModel.ImageFiles);

            bool isEdited = await this.EstateService.EditEstateAsync(id, estateModel);

            if (!isEdited)
            {
                return BadRequest();
            }

            return this.RedirectToAction(nameof(BrokersController.MyEstateOffers), "Brokers");
        }

        public async Task<IActionResult> Delete(string id)
        {
            int brokerId = await BrokerService.GetBrokerIdAsync(this.User.GetLoggedInUserId());

            if (brokerId == 0 && User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            int estateBrokerId = await this.EstateService.GetEstateBrokerId(id);

            if (brokerId != estateBrokerId && !User.IsAdmin())
            {
                return this.Unauthorized();
            }

            bool isDeleted = await this.EstateService.DeleteEstateAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return RedirectToAction("All");
        }

        private async Task<EstateFormModel> LoadEstateFormModel(EstateServiceModel estateModel = null)
        {
            var dropdownData = await this.EstateService.GetDropDownDataAsync();

            EstateFormModel model = new EstateFormModel
            {
                EstateTypeViewModels = dropdownData.EstateTypeModels,
                CurrencyViewModels = dropdownData.CurrencyModels,
                AreasViewModels = dropdownData.Areas,
                TypeOfDeals = dropdownData.TradeTypeModels,
                SelectedFutures = dropdownData.FutureModels.ToList(),
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

        private async Task<List<byte[]>> GetEstateImages(List<IFormFile> inputImages)
        {
            List<byte[]> result = new List<byte[]>();

            if (inputImages == null)
            {
                return result;
            }

            foreach (var imageFile in inputImages)
            {
                await using Stream temp = imageFile.OpenReadStream();

                await using MemoryStream memoryStream = new MemoryStream();

                await temp.CopyToAsync(memoryStream);

                byte[] readedBytes = memoryStream.ToArray();

                result.Add(readedBytes);
            }

            return result;
        }
    }
}
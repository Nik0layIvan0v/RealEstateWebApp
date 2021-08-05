using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;
using RealEstate.Services.Models;

namespace RealEstate.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private protected readonly IEstateService Service;

        public CitiesController(IEstateService service)
        {
            this.Service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<NeighborhoodModel>>> GetCitiesByArea([FromQuery] int id)
        {
            IEnumerable<CityModel> cities = await this.Service.GetCitiesByAreaIdAsync(id);

            if (cities == null)
            {
                return NotFound();
            }

            return new JsonResult(cities);
        }
    }
}
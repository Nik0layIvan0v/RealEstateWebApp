namespace RealEstate.Controllers
{
    using Services;
    using Services.Models;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]

    public class CitiesController : ControllerBase
    {
        private protected readonly IEstateService Service;

        public CitiesController(IEstateService service)
        {
            this.Service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<NeighborhoodViewModel>>> GetCitiesByArea([FromQuery] int id)
        {
            IEnumerable<CityViewModel> cities = await this.Service.GetCitiesByAreaIdAsync(id);

            if (cities == null)
            {
                return NotFound();
            }

            return new JsonResult(cities);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;
using RealEstate.Services.Models;

namespace RealEstate.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]


    public class NeighborhoodsController : ControllerBase
    {
        private protected readonly IEstateService Service;

        public NeighborhoodsController(IEstateService service)
        {
            this.Service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<NeighborhoodModel>>> GetNeighborhoodByCity([FromQuery]int id)
        {
            IEnumerable<NeighborhoodModel> neighborhoods = await this.Service.GetNeighborhoodsByCityIdAsync(id);

            if (neighborhoods == null)
            {
                return NotFound();
            }

            return new JsonResult(neighborhoods);
        }
    }
}

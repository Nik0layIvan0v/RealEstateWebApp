namespace RealEstate.Controllers
{
    using Services;
    using Services.Models;

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

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
        public async Task<ActionResult<IEnumerable<NeighborhoodViewModel>>> GetNeighborhoodByCity([FromQuery]int id)
        {
            IEnumerable<NeighborhoodViewModel> neighborhoods = await this.Service.GetNeighborhoodsByCityIdAsync(id);

            if (neighborhoods == null)
            {
                return NotFound();
            }

            return new JsonResult(neighborhoods);
        }
    }
}

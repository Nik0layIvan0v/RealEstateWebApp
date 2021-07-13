using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class ReportController : Controller
    {
        public ReportController()
        {
            
        }

        /// <summary>
        /// Visuals report form
        /// </summary>
        public IActionResult Create(string Id)
        {
            return Ok();
        }

        /// <summary>
        /// Send reported information
        /// </summary>
        [HttpPost]
        public IActionResult Create() //<<=== imput model!
        {
            return Ok();
        }
    }
}

namespace RealEstate.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        public IActionResult Search() //<= show search form
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchParameters) //<== search parameters
        {
            return View();
        }
    }
}

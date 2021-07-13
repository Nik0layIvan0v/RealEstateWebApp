using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class EstateController : Controller
    {
        public EstateController() //<== service
        {
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(string model) //<= model from form!
        {
            return this.Redirect("/"); //<= redirect to my offers!
        }

        public IActionResult Details()
        {
            return this.View();
        }

        public IActionResult Edit(string id)
        {
            return this.View();
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
    }
}

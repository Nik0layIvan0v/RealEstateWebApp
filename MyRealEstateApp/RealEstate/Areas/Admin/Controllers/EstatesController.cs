namespace RealEstate.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class EstatesController : AdminController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}

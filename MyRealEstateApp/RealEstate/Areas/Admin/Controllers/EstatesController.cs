namespace RealEstate.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static RealEstate.Areas.Admin.AdminConstants;
    using static RealEstate.Common.WebConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class EstatesController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}

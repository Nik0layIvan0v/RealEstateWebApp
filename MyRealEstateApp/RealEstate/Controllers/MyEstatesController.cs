using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class MyEstatesController : Controller
    {
        public MyEstatesController()
        {
            
        }
        public async Task<IActionResult> Wishlist()
        {
            return this.Ok();
        }

        public async Task<IActionResult> AddToWishlist(string adId)
        {
            return this.Ok();
        }
    }
}

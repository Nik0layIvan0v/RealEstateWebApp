using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    using System.Threading.Tasks;

    public class CommentController : Controller
    {
        public CommentController()
        {


        }

        public async Task<IActionResult> GetAllCommentsByEstateId(string id)
        {
            object comment = new {comment = "i am comment"};

            return this.Json(comment);
        }

        [HttpPost]
        public IActionResult PostComment() //<== commentModel
        {
            return this.Redirect("/");
        }
    }
}

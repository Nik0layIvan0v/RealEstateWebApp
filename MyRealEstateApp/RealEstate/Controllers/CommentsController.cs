namespace RealEstate.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RealEstate.Services;
    using System;
    using RealEstate.Infrastructure;

    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        // GET: CommentsController/Details/5
        public ActionResult Details(string estateId)
        {
            return View();
        }

        // GET: CommentsController/Create
        public ActionResult CreateCommnet([FromQuery] string estateId)
        {
            return this.View();
        }

        // POST: CommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCommnet(IFormCollection collection)
        {
            return Ok();
        }

        // GET: CommentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: CommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string estateId)
        {
            string userId = this.User.GetLoggedInUserId();

            try
            {
                this.commentService.DeleteComment(estateId, userId);

                return RedirectToAction(nameof(EstatesController.Details), "Estates", new { id = estateId });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

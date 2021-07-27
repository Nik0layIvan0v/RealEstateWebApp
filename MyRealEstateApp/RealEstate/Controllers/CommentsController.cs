namespace RealEstate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RealEstate.Services;
    using System;
    using RealEstate.Infrastructure;
    using RealEstate.Models.Comments;
    using RealEstate.Models;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService CommentService;

        public CommentsController(ICommentService commentService)
        {
            this.CommentService = commentService;
        }

        public ActionResult CreateCommnet([FromQuery] string estateId)
        {
            CommentFormModel formModel = new()
            {
                EstateId = estateId
            };

            return this.View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCommnet(CommentFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return CreateCommnet(formModel);
            }

            var userId = this.User.GetLoggedInUserId();

            Comment comment = new()
            {
                UserId = userId,
                EstateId = formModel.EstateId,
                CommentContent = formModel.Content
            };

            this.CommentService.AddComment(comment);

            return RedirectToAction(nameof(EstatesController.Details), "Estates", new { Id = comment.EstateId });
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentFormModel formModel)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string estateId)
        {
            string userId = this.User.GetLoggedInUserId();

            try
            {
                this.CommentService.DeleteComment(estateId, userId);

                return RedirectToAction(nameof(EstatesController.Details), "Estates", new { id = estateId });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

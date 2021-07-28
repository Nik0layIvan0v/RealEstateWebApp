namespace RealEstate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RealEstate.Services;
    using System.Threading.Tasks;
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

        public ActionResult Create(string Id)
        {
            CommentFormModel formModel = new CommentFormModel()
            {
                EstateId = Id
            };

            return this.View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CommentFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return await Create(formModel);
            }

            var userId = this.User.GetLoggedInUserId();

            Comment comment = new()
            {
                UserId = userId,
                EstateId = formModel.EstateId,
                CommentContent = formModel.Content
            };

            bool isAdded = await this.CommentService.AddComment(comment);

            if (!isAdded)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(EstatesController.Details), "Estates", new { Id = formModel.EstateId });
        }

        public async Task<ActionResult> Edit(string id)
        {
            var userId = this.User.GetLoggedInUserId();

            bool isCommentOwnedByUser = await this.CommentService.IsUserOwnCommentAsync(id, userId);

            if (!isCommentOwnedByUser)
            {
                return Unauthorized();
            }

            var commentData = await this.CommentService.GetCommentById(id);

            CommentFormModel model = new()
            {
                Content = commentData.CommentContent,
                EstateId = commentData.EstateId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, CommentFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return await Edit(id, formModel);
            }

            var userId = this.User.GetLoggedInUserId();

            bool isCommentOwnedByUser = await this.CommentService.IsUserOwnCommentAsync(id, userId);

            if (!isCommentOwnedByUser)
            {
                return Unauthorized();
            }

            Comment comment = new()
            {
                EstateId = formModel.EstateId,
                CommentContent = formModel.Content,
                UserId = userId
            };

            await this.CommentService.EditComment(comment);

            return RedirectToAction(nameof(EstatesController.Details), "Estates", new { Id = formModel.EstateId });
        }

        public async Task<ActionResult> Delete(string id)
        {
            string userId = this.User.GetLoggedInUserId();

            if (userId == null)
            {
                return BadRequest();
            }

            var comment = await this.CommentService.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            bool isUserOwnComment = await this.CommentService.IsUserOwnCommentAsync(userId, comment.EstateId);

            bool isDeleted = await this.CommentService.DeleteComment(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(EstatesController.Details), "Estates", new { id = comment.EstateId });
        }
    }
}

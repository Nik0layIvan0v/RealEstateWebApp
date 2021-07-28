namespace RealEstate.Services
{
    using Microsoft.EntityFrameworkCore;
    using RealEstate.Data;
    using RealEstate.Models;
    using RealEstate.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentService : ICommentService
    {
        private readonly RealEstateDbContext Context;

        public CommentService(RealEstateDbContext context)
        {
            this.Context = context;
        }

        public async Task<bool> AddComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException();
            }

            this.Context.Comments.Add(comment);

            int countOfChanges = await this.Context.SaveChangesAsync();

            if (countOfChanges == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EditComment(Comment comment)
        {
            var dbComment = await this.Context.Comments.FirstOrDefaultAsync(x => x.EstateId == comment.EstateId && x.UserId == comment.UserId);

            if (dbComment == null)
            {
                return false;
            }

            dbComment.CommentContent = comment.CommentContent;

            await this.Context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CommentServiceModel>> GetCommentsByEstateId(string estateId)
        {
            var dbcomments = await this.Context.Comments
                 .Where(est => est.EstateId == estateId)
                 .Select(x => new CommentServiceModel
                 {
                     CommentId = x.Id,
                     CommentContent = x.CommentContent,
                     EstateId = x.EstateId,
                     CreatorName = this.Context.Users.FirstOrDefault(ui => ui.Id == x.UserId).Email
                 })
                 .ToArrayAsync();

            return dbcomments;
        }

        public async Task<CommentServiceModel> GetCommentById(string commentId)
        {
            return await this.Context
                .Comments
                .Select(comment => new CommentServiceModel
                {
                    CommentId = comment.Id,
                    CommentContent = comment.CommentContent,
                    CreatorName = this.Context.Users.FirstOrDefault(ui => ui.Id == comment.UserId).Email,
                    EstateId = comment.EstateId
                })
                .FirstAsync(c => c.CommentId == commentId);
        }

        public async Task<bool> IsUserOwnCommentAsync(string commentId, string userId)
        {
            return await this.Context.Comments.AnyAsync(c => c.Id == commentId && c.UserId == userId);
        }

        public async Task<bool> DeleteComment(string commentId)
        {
            Comment comment = await this.Context.Comments.FindAsync(commentId);

            if (comment == null)
            {
                return false;
            }

            this.Context.Comments.Remove(comment);

            int removedItem = await this.Context.SaveChangesAsync();

            if (removedItem == 0)
            {
                return false;
            }

            return true;
        }
    }
}

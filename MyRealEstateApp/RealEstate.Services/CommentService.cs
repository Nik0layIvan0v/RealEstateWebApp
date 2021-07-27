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

        public bool AddComment(Comment comment)
        {
            this.Context.Comments.Add(comment);

            int countOfChanges = this.Context.SaveChanges();

            if (countOfChanges == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteComment(string estateId, string commentId)
        {
            throw new NotImplementedException();
        }

        public bool EditComment(string estateId, Comment comment)
        {
            throw new NotImplementedException();
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
                     CreatorName = Context.Users.FirstOrDefault(ui => ui.Id == x.UserId).Email
                 })
                 .ToArrayAsync();

            return dbcomments;
        }
    }
}

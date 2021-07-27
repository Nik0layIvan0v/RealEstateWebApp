namespace RealEstate.Services
{
    using RealEstate.Models;
    using RealEstate.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        bool AddComment(Comment comment);

        bool EditComment(string estateId, Comment comment);

        bool DeleteComment(string estateId, string commentId);

        Task<IEnumerable<CommentServiceModel>> GetCommentsByEstateId(string estateId);
    }
}

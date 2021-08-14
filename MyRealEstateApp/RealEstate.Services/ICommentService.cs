namespace RealEstate.Services
{
    using RealEstate.Models;
    using RealEstate.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<bool> AddComment(Comment comment);

        Task<bool> EditComment(Comment comment);

        Task<bool> DeleteComment(string commentId);

        Task<IEnumerable<CommentServiceModel>> GetCommentsByEstateId(string estateId);

        Task<CommentServiceModel> GetCommentById(string commentId);

        Task<bool> IsUserOwnCommentAsync(string commentId, string userId);
    }
}

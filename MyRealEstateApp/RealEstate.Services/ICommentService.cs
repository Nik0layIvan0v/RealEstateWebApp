namespace RealEstate.Services
{
    using RealEstate.Models;

    public interface ICommentService
    {
        bool AddComment(string estateId, Comment comment);

        bool EditComment(string estateId, Comment comment);

        bool DeleteComment(string estateId, string commentId);
    }
}

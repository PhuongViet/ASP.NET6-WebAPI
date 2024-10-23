using FINSHARK2.DTOs.Stock;
using FINSHARK2.Models;

namespace FINSHARK2.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllCommentAsync();
        public Task<Comment> CreateCommentAsync(Comment createComment);
        public Task<Comment> GetCommentByIdAsync(int id);

        public Task<Comment?> UpdateCommentAsync(int id, Comment commentModel);
        public Task<Comment> DeleteCommentAsync(int id);
    }
}

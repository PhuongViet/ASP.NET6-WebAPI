using FINSHARK2.Data;
using FINSHARK2.DTOs.Comment;
using FINSHARK2.Interfaces;
using FINSHARK2.Models;
using Microsoft.EntityFrameworkCore;

namespace FINSHARK2.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Comment> CreateCommentAsync(Comment createComment)
        {
            await dbContext.Comments.AddAsync(createComment);
            await dbContext.SaveChangesAsync();
            return createComment;
        }

        public async Task<Comment> DeleteCommentAsync(int id)
        {
            var commentModel =await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            
            dbContext.Comments.Remove(commentModel);
            await dbContext.SaveChangesAsync();
            return(commentModel);
        }

        public async Task<List<Comment>> GetAllCommentAsync()
        {
            var Comments =await dbContext.Comments.ToListAsync();
            return Comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var comment =await dbContext.Comments.FindAsync(id);
            if (comment == null) {
                return null;
            }
            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
            var existingComment = await dbContext.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await dbContext.SaveChangesAsync();
            return existingComment;
        }
    }
}

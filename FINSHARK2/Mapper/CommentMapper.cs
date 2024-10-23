using FINSHARK2.DTOs.Comment;
using FINSHARK2.Models;

namespace FINSHARK2.Mapper
{
    public static class CommentMapper
    {
        public static CommentDTO toCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

        public static Comment toCommentFromCreateDTO(this CreateCommentRequestDTO createComment, int stockId) {
            return new Comment
            {
                Title = createComment.Title,
                Content = createComment.Content,
                StockId= stockId
            };
        }

        public static Comment toCommentFromUpdatetDTO(this UpdateCommentRequestDTO updateComment)
        {
            return new Comment
            {
                Title = updateComment.Title,
                Content = updateComment.Content
            };
        }

    }
}

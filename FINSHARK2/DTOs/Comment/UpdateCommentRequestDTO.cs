﻿using System.ComponentModel.DataAnnotations;

namespace FINSHARK2.DTOs.Comment
{
    public class UpdateCommentRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 character")]
        [MaxLength(280, ErrorMessage = "Title cannot be Over 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 character")]
        [MaxLength(280, ErrorMessage = "Content cannot be Over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}

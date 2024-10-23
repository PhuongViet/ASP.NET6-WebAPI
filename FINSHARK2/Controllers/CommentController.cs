using FINSHARK2.Data;
using FINSHARK2.DTOs.Comment;
using FINSHARK2.Interfaces;
using FINSHARK2.Mapper;
using FINSHARK2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FINSHARK2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICommentRepository commentRepository;
        private readonly IStockRepository stockRepository;

        public CommentController(ApplicationDbContext dbContext,ICommentRepository commentRepository,IStockRepository stockRepository)
        {
            this.dbContext = dbContext;
            this.commentRepository = commentRepository;
            this.stockRepository = stockRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var comment = await commentRepository.GetAllCommentAsync();
            var commentDTO = comment.Select(c => c.toCommentDTO());
            return Ok(commentDTO);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId,[FromBody] CreateCommentRequestDTO commentDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (!await stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exits");
            }

            var commentModel = commentDTO.toCommentFromCreateDTO(stockId);
            await commentRepository.CreateCommentAsync(commentModel);
            /* return CreatedAtAction(nameof(GetCommentById), new { id = commentModel }, commentModel.toCommentDTO());
 */
            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel.toCommentDTO());


        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var comment = await commentRepository.GetCommentByIdAsync(id);
            if (comment == null) {
                return NotFound();
            }
            return Ok(comment);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDTO updateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var comment = await commentRepository.UpdateCommentAsync(id, updateDTO.toCommentFromUpdatetDTO());

            if (comment == null) {
                return NotFound("Comment Not Found");
            }

            return Ok(comment.toCommentDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var comment =await commentRepository.DeleteCommentAsync(id);
            if (comment == null)
            {
                return NotFound("Comment Not Found");
            }

            return Ok(comment);
        }
    }
}

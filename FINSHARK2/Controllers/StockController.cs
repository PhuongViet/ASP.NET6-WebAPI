using FINSHARK2.Data;
using FINSHARK2.DTOs.Stock;
using FINSHARK2.Helpers;
using FINSHARK2.Interfaces;
using FINSHARK2.Mapper;
using FINSHARK2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FINSHARK2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IStockRepository stockRepository;

        public StockController(ApplicationDbContext dbContext, IStockRepository stockRepository) {
            this.dbContext = dbContext;
            this.stockRepository = stockRepository;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllStock([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var stocks = await stockRepository.GetAllStockAsync(query);
            var stockDTOS = stocks.Select(s => s.ToStockDTO());
            return Ok(stockDTOS);
        }


        [HttpGet]
        [Route("{id:int}")]
        

        public async Task<IActionResult> GetStockById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var stock = await stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return null;
            }
            return Ok(stock);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDTO stockDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var stock = stockDTO.ToStockFromCreateDTO();
            await stockRepository.CreateStockAsync(stock);
            return Ok(stock);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id ,[FromBody] UpdateStockRequestDTO updateStock)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var stock = await stockRepository.UpdateStockAsync(id, updateStock);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var stock =await stockRepository.DeleteStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

    }
}

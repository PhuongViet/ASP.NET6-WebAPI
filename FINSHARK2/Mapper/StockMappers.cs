
using FINSHARK2.DTOs.Stock;
using FINSHARK2.Models;

namespace FINSHARK2.Mapper
{
    public static class StockMappers
    {
        public static StockDTO ToStockDTO(this Stock stock) {
            return new StockDTO
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(c=>c.toCommentDTO()).ToList()
            };
        
        }

        public static Stock ToStockFromCreateDTO (this CreateStockRequestDTO createStock)
        {
            return new Stock
            {
                Symbol = createStock.Symbol,
                CompanyName = createStock.CompanyName,
                Purchase = createStock.Purchase,
                LastDiv = createStock.LastDiv,
                Industry = createStock.Industry,
                MarketCap = createStock.MarketCap
            };
        }

    }
}

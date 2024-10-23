using FINSHARK2.Data;
using FINSHARK2.DTOs.Stock;
using FINSHARK2.Helpers;
using FINSHARK2.Interfaces;
using FINSHARK2.Models;
using Microsoft.EntityFrameworkCore;

namespace FINSHARK2.Repository
{

    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StockRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
             await dbContext.Stocks.AddAsync(stock);
             await dbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteStockByIdAsync(int id)
        {
            var stock = await dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null) {
                return null;
            }

            dbContext.Stocks.Remove(stock);
            await dbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllStockAsync(QueryObject query)
        {
            var stocks = dbContext.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            { 
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s =>s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;


            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock> GetStockByIdAsync(int id)
        {
            var stock = await dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
            if (stock == null) {
                return null;
            }
            return stock;
        }

        public Task<bool> StockExists(int id)
        {
            return dbContext.Stocks.AnyAsync(c => c.Id == id);
        }

        public async Task<Stock> UpdateStockAsync(int id, UpdateStockRequestDTO updateStock)
        {
            var stock = await dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }

            stock.Symbol= updateStock.Symbol;
            stock.CompanyName = updateStock.CompanyName;
            stock.Purchase = updateStock.Purchase;
            stock.LastDiv = updateStock.LastDiv;
            stock.Industry= updateStock.Industry;
            stock.MarketCap=updateStock.MarketCap;

            await dbContext.SaveChangesAsync();
            return stock;
        }

    }
}

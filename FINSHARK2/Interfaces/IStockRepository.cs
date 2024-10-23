using FINSHARK2.DTOs.Stock;
using FINSHARK2.Helpers;
using FINSHARK2.Models;

namespace FINSHARK2.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllStockAsync(QueryObject query) ;
        public Task<Stock> CreateStockAsync(Stock stock) ;
        public Task<Stock> GetStockByIdAsync(int id) ;
        public Task<Stock> UpdateStockAsync(int id, UpdateStockRequestDTO updateStock);

        public Task<Stock> DeleteStockByIdAsync(int id);
        public Task<bool> StockExists(int id);
    }
}

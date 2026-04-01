using DataTransferObjects.StockMovement;
using Models;

namespace SalesManager.Web.Interfaces
{
    public interface IStockMovementService
    {
        Task<List<StockMovementGetDTO>> GetStockMovementAsync(string requestUri);

        Task<StockMovementGetDTO> GetStockMovementByIdAsync(string requestUri);

        Task<bool> CreatePurchaseAsync(string requestUri, StockMovementPurchasePostDTO stockMovementPostDTO);
        Task<bool> CreateSaleAsync(string requestUri, StockMovementSalePostDTO stockMovementPostDTO);

        Task<bool> DeleteAsync(string requestUri);
    }
}

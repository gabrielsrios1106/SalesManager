using DataTransferObjects.StockMovement;
using Models;

namespace SalesManager.API.Interfaces
{
    public interface IStockMovementService
    {
        Task<List<StockMovementGetDTO>> GetStockMovementAsync(string value, int idUser);

        Task<StockMovement> GetStockMovementByIdAsync(int stockMovementId);

        Task InsertAsync(StockMovement stockMovement);

        Task DeleteAsync(StockMovement stockMovement);
    }
}

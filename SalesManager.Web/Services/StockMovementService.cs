using DataTransferObjects.StockMovement;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IAPIService _apiService;

        public StockMovementService(IAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<StockMovementGetDTO>> GetStockMovementAsync(string requestUri)
        {
            return await _apiService.GetListAsync<StockMovementGetDTO>($"v1/{requestUri}");
        }

        public async Task<StockMovementGetDTO> GetStockMovementByIdAsync(string requestUri)
        {
            return await _apiService.GetByIdAsync<StockMovementGetDTO>($"v1/{requestUri}");
        }

        public async Task<bool> CreatePurchaseAsync(string requestUri, StockMovementPurchasePostDTO stockMovementPostDTO)
        {
            return await _apiService.CreateAsync($"v1/{requestUri}", stockMovementPostDTO);
        }

        public async Task<bool> CreateSaleAsync(string requestUri, StockMovementSalePostDTO stockMovementPostDTO)
        {
            return await _apiService.CreateAsync($"v1/{requestUri}", stockMovementPostDTO);
        }

        public async Task<bool> DeleteAsync(string requestUri)
        {
            return await _apiService.DeleteAsync($"v1/{requestUri}");
        }
    }
}

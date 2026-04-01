using DataTransferObjects.Departments;
using DataTransferObjects.FinancialManager;
using MudBlazor;
using SalesManager.Web.Interfaces;


namespace SalesManager.Web.Services
{
    public class FinancialManagerService : IFinancialManagerService
    {

        private readonly IAPIService _apiService;

        public FinancialManagerService(IAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<FinancialManagerGetDTO>> GetFinancialManagersAsync(string requestUri)
        {
            return await _apiService.GetListAsync<FinancialManagerGetDTO>($"v1/{requestUri}");
        }

        public async Task<BalanceGetDTO> GetBalanceAsync(string requestUri)
        {
            return await _apiService.GetByIdAsync<BalanceGetDTO>($"v1/{requestUri}");
        }
    }
}

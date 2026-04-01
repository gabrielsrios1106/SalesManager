using DataTransferObjects.FinancialManager;
using Models;

namespace SalesManager.API.Interfaces
{
    public interface IFinancialManagerService
    {
        Task<List<FinancialManagerGetDTO>> GetFinancialManagersAsync(int idUser);

        Task<FinancialManager> GetFinancialManagerById(int productId);

        Task<BalanceGetDTO> GetBalanceAsync(int idUser);

        Task InsertAsync(int productId, int userId);

        Task UpdateAsync(int productId);

        Task DeleteAsync(FinancialManager financialManager);
    }
}

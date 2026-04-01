using DataTransferObjects.Departments;
using DataTransferObjects.FinancialManager;
using System.Threading.Tasks;

namespace SalesManager.Web.Interfaces
{
    public interface IFinancialManagerService
    {
        Task<List<FinancialManagerGetDTO>> GetFinancialManagersAsync(string requestUri);

        Task<BalanceGetDTO> GetBalanceAsync(string requestUri);
    }
}

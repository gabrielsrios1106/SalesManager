using Models;

namespace SalesManager.API.Interfaces
{
    public interface IRegisterService
    {
        Task InsertAsync(User register);

        Task<bool> ExistsAsync(int registerId);
        Task<bool> ExistsByEmailAsync(string registerEmail);
                    
    }
}

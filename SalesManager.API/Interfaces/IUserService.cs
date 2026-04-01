using DataTransferObjects.Utils;

namespace SalesManager.API.Interfaces
{
    public interface IUserService
    {
        Task<string> CheckAccess(LoginFormPostDTO loginFormGetDTO);
    }
}

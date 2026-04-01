using DataTransferObjects.Utils;

namespace SalesManager.Web.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginFormPostDTO loginFormPostDTO);
    }
}

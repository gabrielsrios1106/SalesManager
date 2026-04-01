using DataTransferObjects.Utils;

namespace SalesManager.Web.Interfaces
{
    public interface IRegisterService
    {
        Task<bool> CreateAsync(string requestUri, UserPostDTO registerPostDTO);
    }
}

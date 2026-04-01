using DataTransferObjects.Products;
using DataTransferObjects.Utils;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IAPIService _apiService;

        public RegisterService(IAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<bool> CreateAsync(string requestUri, UserPostDTO registerPostDTO)
        {
            return await _apiService.CreateAsync($"v1/{requestUri}", registerPostDTO);
        }
    }
}

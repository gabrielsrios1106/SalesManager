using DataTransferObjects.Products;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IAPIService _apiService;

        public ProductService(IAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<ProductGetDTO>> GetProductsAsync(string requestUri)
        {
            return await _apiService.GetListAsync<ProductGetDTO>($"v1/{requestUri}");
        }

        public async Task<ProductGetDTO> GetProductByIdAsync(string requestUri)
        {
            return await _apiService.GetByIdAsync<ProductGetDTO>($"v1/{requestUri}");
        }

        public async Task<bool> CreateAsync(string requestUri, ProductPostDTO productPostDTO)
        {
            return await _apiService.CreateAsync($"v1/{requestUri}", productPostDTO);
        }

        public async Task<bool> UpdateAsync(string requestUri, ProductPutDTO productPutDTO)
        {
            return await _apiService.UpdateAsync($"v1/{requestUri}", productPutDTO);
        }

        public async Task<bool> DeleteAsync(string requestUri)
        {
            return await _apiService.DeleteAsync($"v1/{requestUri}");
        }
    }
}

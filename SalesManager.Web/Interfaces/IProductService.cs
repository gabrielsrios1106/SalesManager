using DataTransferObjects.Products;

namespace SalesManager.Web.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductGetDTO>> GetProductsAsync(string requestUri);

        Task<ProductGetDTO> GetProductByIdAsync(string requestUri);

        Task<bool> CreateAsync(string requestUri, ProductPostDTO productPostDTO);

        Task<bool> UpdateAsync(string requestUri, ProductPutDTO productPutDTO);

        Task<bool> DeleteAsync(string requestUri);
    }
}

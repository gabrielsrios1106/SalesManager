using DataTransferObjects.Products;
using Models;

namespace SalesManager.API.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductGetDTO>> GetProductAsync(string value, int idUser, string orderBy, bool showInactive);

        Task<Product> GetProductByIdAsync(int productId);

        bool SufficientStock(int productId, int quantity);

        Task InsertAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);

        Task<bool> ExistsAsync(int productId, int idUser);

        Task<Product> ExistsByNameAsync(string productName, int idUser);

        Task<Product> ExistsByNameUpdateAsync(string productName, int productId, int idUser);

        Task<bool> ExistsStockMovement(int productId, int idUser);
    }
}
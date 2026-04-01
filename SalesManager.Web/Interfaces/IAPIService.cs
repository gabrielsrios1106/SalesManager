namespace SalesManager.Web.Interfaces
{
    public interface IAPIService
    {
        Task<List<T>> GetListAsync<T>(string requestUri);

        Task<T> GetByIdAsync<T>(string requestUri);

        Task<bool> CreateAsync<T>(string requestUri, T value);
        
        Task<bool> UpdateAsync<T>(string requestUri, T value);

        Task<bool> DeleteAsync(string requestUri);
    }
}

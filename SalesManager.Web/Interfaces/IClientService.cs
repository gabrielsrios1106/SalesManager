using DataTransferObjects.Clients;

namespace SalesManager.Web.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientGetDTO>> GetClientsAsync(string requestUri);

        Task<ClientGetDTO> GetClientByIdAsync(string requestUri);

        Task<bool> CreateAsync(string requestUri, ClientPostDTO clientPostDTO);

        Task<bool> UpdateAsync(string requestUri, ClientPutDTO clientPutDTO);

        Task<bool> DeleteAsync(string requestUri);
    }
}

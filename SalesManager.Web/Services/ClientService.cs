using DataTransferObjects.Clients;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Services
{
    public class ClientService : IClientService
    {

        private readonly IAPIService _apiService;

        public ClientService(IAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<ClientGetDTO>> GetClientsAsync(string requestUri)
        {
            return await _apiService.GetListAsync<ClientGetDTO>($"v1/{requestUri}");
        }

        public async Task<ClientGetDTO> GetClientByIdAsync(string requestUri)
        {
            return await _apiService.GetByIdAsync<ClientGetDTO>($"v1/{requestUri}");
        }

        public async Task<bool> CreateAsync(string requestUri, ClientPostDTO clientPostDTO)
        {
            return await _apiService.CreateAsync($"v1/{requestUri}", clientPostDTO);
        }

        public async Task<bool> UpdateAsync(string requestUri, ClientPutDTO clientPutDTO)
        {
            return await _apiService.UpdateAsync($"v1/{requestUri}", clientPutDTO);
        }

        public async Task<bool> DeleteAsync(string requestUri)
        {
            return await _apiService.DeleteAsync($"v1/{requestUri}");
        }

    }
}

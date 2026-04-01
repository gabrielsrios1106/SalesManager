using DataTransferObjects.Clients;
using Models;

namespace SalesManager.API.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientGetDTO>> GetClientsAsync(string value, int idUser, bool showInactive);

        Task<Client> GetClientByIdAsync(int clientId);

        Task InsertAsync(Client client);

        Task UpdateAsync(Client client);

        Task DeleteAsync(Client client);

        Task<bool> ExistsAsync(int clientId, int idUser);

        Task<bool> ExistsByEmailAsync(string clientName, int idUser);

        Task<bool> ExistsByEmailUpdateAsync(string clientName, int clientId, int idUser);

        Task<bool> ExistsStockMovement(int clientId, int idUser);
    }
}

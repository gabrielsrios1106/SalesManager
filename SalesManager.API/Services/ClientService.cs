using DataTransferObjects.Clients;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Services
{
    public class ClientService : IClientService
    {
        private readonly SalesManagerContext _context;

        public ClientService(SalesManagerContext context)
        {
            _context = context;
        }

        public async Task<List<ClientGetDTO>> GetClientsAsync(string value, int idUser, bool showInactive)
        {
            IQueryable<Client> queryable = _context.Client
                                                       .AsNoTracking()
                                                       .AsSplitQuery()
                                                       .Where(c => c.UserId == idUser)
                                                       .OrderByDescending(d => d.CreatedAt);
            if (!showInactive)
            {
                queryable = queryable.Where(d => d.Status == 1);
            }

            if (!string.IsNullOrEmpty(value))
            {
                queryable = queryable.Where(d => d.ClientName.ToLower().Contains(value.ToLower()));
            }

            List<ClientGetDTO> clientsGetDTO = await queryable.Select(c => new ClientGetDTO()
            {
                Id = c.Id,
                ClientName = c.ClientName,
                ClientEmail = c.ClientEmail,
                ClientAddressCity = c.ClientAddressCity,
                ClientAddressState = c.ClientAddressState,
                ClientCEP = c.ClientCEP,
                CreatedAt = c.CreatedAt,
                Status = c.Status
            }).ToListAsync();

            return clientsGetDTO;
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _context.Client
                                 .AsNoTracking()
                                 .AsSplitQuery()
                                 .FirstOrDefaultAsync(d => d.Id == clientId);
        }

        public async Task InsertAsync(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            try
            {
                _context.Entry(client).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException($"Erro ao atualizar: {e.Message}");
            }
        }

        public async Task DeleteAsync(Client client)
        {
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int clientId, int idUser) => await _context.Client.AsNoTracking().AnyAsync(d => d.Id == clientId && d.UserId == idUser);

        public async Task<bool> ExistsByEmailAsync(string clientEmail, int idUser) => await _context.Client.AsNoTracking().AnyAsync(d => d.ClientEmail.ToLower() == clientEmail.ToLower() && d.UserId == idUser);

        public async Task<bool> ExistsByEmailUpdateAsync(string clientEmail, int clientId, int idUser) => await _context.Client.AsNoTracking().AnyAsync(d => d.ClientEmail.ToLower() == clientEmail.ToLower() && d.Id != clientId && d.UserId == idUser);

        public async Task<bool> HasProduct(int departmentId, int idUser) => await _context.Product.AsNoTracking().AnyAsync(a => a.DepartmentId == departmentId && a.UserId == idUser);

        public async Task<bool> ExistsStockMovement(int clientId, int idUser) => await _context.StockMovement.AsNoTracking().AnyAsync(sm => sm.ClientID == clientId && sm.UserId == idUser);

    }
}

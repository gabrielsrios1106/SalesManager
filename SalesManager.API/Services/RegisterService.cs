using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;

namespace SalesManager.API.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly SalesManagerContext _context;

        public RegisterService(SalesManagerContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(User register)
        {
            _context.User.Add(register);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int registerId) => await _context.User.AsNoTracking().AnyAsync(p => p.Id == registerId);

        public async Task<bool> ExistsByEmailAsync(string registerEmail) => await _context.User.AsNoTracking().AnyAsync(p => p.Email.ToLower() == registerEmail.ToLower());
    }
}

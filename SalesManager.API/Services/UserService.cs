using DataTransferObjects.Utils;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;

namespace SalesManager.API.Services
{
    public class UserService : IUserService
    {
        private readonly SalesManagerContext _context;

        public UserService(SalesManagerContext context)
        {
            _context = context;
        }

        public async Task<string> CheckAccess(LoginFormPostDTO loginFormPostDTO)
        {
            string result = string.Empty;

            User user = await _context.User
                                      .AsNoTracking()
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync(u => u.Email == loginFormPostDTO.Email && u.Password == loginFormPostDTO.Password);

            if (user != null)
            {
                result = $"{user.CompleteName}|{user.Id}";
            }

            return result;
        }
    }
}

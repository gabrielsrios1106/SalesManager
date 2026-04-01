using DataTransferObjects.Products;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Services
{
    public class ProductService : IProductService
    {
        private readonly SalesManagerContext _context;
        private readonly IFinancialManagerService _financialManagerService;

        public ProductService(SalesManagerContext context, IFinancialManagerService financialManagerService)
        {
            _context = context;
            _financialManagerService = financialManagerService;
        }

        public async Task<List<ProductGetDTO>> GetProductAsync(string value, int idUser, string orderBy, bool showInactive)
        {
            IQueryable<Product> queryable = _context.Product
                                                    .AsNoTracking()
                                                    .Include(p => p.Department)
                                                    .AsSplitQuery()
                                                    .Where(c => c.UserId == idUser);

            if (string.IsNullOrEmpty(orderBy))
            {
                queryable = queryable.OrderByDescending(p => p.CreatedAt);
            }
            else if (orderBy == "ProductName")
            {
                queryable = queryable.OrderBy(p => p.ProductName);
            }

            if (!showInactive)
            {
                queryable = queryable.Where(d => d.Status == 1);
            }

            if (!string.IsNullOrEmpty(value))
            {
                queryable = queryable.Where(p => p.ProductName.ToLower().Contains(value.ToLower()));
            }

            List<ProductGetDTO> productsGetDTO = await queryable.Select(p => new ProductGetDTO()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                MinimumStock = p.MinimumStock,
                BalanceStock = p.BalanceStock,
                CreatedAt = p.CreatedAt,
                DepartmentId = p.DepartmentId,
                DepartmentName = p.Department.DepartmentName,
                Status = p.Status
            }).ToListAsync();

            return productsGetDTO;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Product
                                 .AsNoTracking()
                                 .AsSplitQuery()
                                 .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public bool SufficientStock(int productId, int quantity)
        {
            int stockBalance = _context.Product.FirstOrDefault(p => p.Id == productId).BalanceStock;

            return stockBalance >= quantity;
        }

        public async Task InsertAsync(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            await _financialManagerService.InsertAsync(product.Id, product.UserId);
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException($"Erro ao atualizar: {e.Message}");
            }
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int productId, int idUser) => await _context.Product.AsNoTracking().AnyAsync(p => p.Id == productId && p.UserId == idUser);

        public async Task<Product> ExistsByNameAsync(string productName, int idUser) => await _context.Product.AsNoTracking().FirstOrDefaultAsync(p => p.ProductName.ToLower() == productName.ToLower() && p.UserId == idUser);

        public async Task<Product> ExistsByNameUpdateAsync(string productName, int productId, int idUser) => await _context.Product.AsNoTracking().FirstOrDefaultAsync(d => d.ProductName.ToLower() == productName.ToLower() && d.Id != productId && d.UserId == idUser);

        public async Task<bool> ExistsStockMovement(int productId, int idUser) => await _context.StockMovement.AsNoTracking().AnyAsync(sm => sm.ProductId == productId && sm.UserId == idUser);
    }
}

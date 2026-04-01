using DataTransferObjects.StockMovement;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly SalesManagerContext _context;
        private readonly IProductService _productService;
        private readonly IFinancialManagerService _financialManagerService;

        public StockMovementService(SalesManagerContext context, IProductService productService, IFinancialManagerService financialManagerService)
        {
            _context = context;
            _productService = productService;
            _financialManagerService = financialManagerService;
        }

        public async Task<List<StockMovementGetDTO>> GetStockMovementAsync(string value, int idUser)
        {
            IQueryable<StockMovement> queryable = _context.StockMovement
                                                          .AsNoTracking()
                                                          .Include(p => p.Product).ThenInclude(p => p.Department).AsSplitQuery()
                                                          .Include(c => c.Client).AsSplitQuery()
                                                          .Where(c => c.UserId == idUser)
                                                          .OrderByDescending(p => p.CreatedAt);

            if (!string.IsNullOrEmpty(value))
            {
                queryable = queryable.Where(p =>
                                                p.Product.ProductName.ToLower().Contains(value.ToLower()) ||
                                                p.Product.Department.DepartmentName.ToLower().Contains(value.ToLower()) ||
                                                p.Client.ClientName.ToLower().Contains(value.ToLower())
                                           );
            }

            List<StockMovementGetDTO> stockMovementGetDTO = await queryable.Select(sm => new StockMovementGetDTO()
            {
                Id = sm.Id,
                MovementType = sm.MovementType,
                Message = sm.Message,
                Quantity = sm.Quantity,
                CreatedAt = sm.CreatedAt,
                ProductId = sm.ProductId,
                ClientId = sm.ClientID,
                ProductName = sm.Product.ProductName,
                MovementValue = sm.MovementValue,
                ClientName = sm.Client.ClientName
            }).ToListAsync();

            return stockMovementGetDTO;
        }

        public async Task<StockMovement> GetStockMovementByIdAsync(int stockMovementId)
        {
            return await _context.StockMovement
                                 .AsNoTracking()
                                 .Include(p => p.Product).ThenInclude(p => p.Department).AsSplitQuery()
                                 .Include(c => c.Client).AsSplitQuery()
                                 .FirstOrDefaultAsync(p => p.Id == stockMovementId);
        }

        public async Task InsertAsync(StockMovement stockMovement)
        {
            //double price = _context.Product.FirstOrDefault(p => p.Id == stockMovement.ProductId).Price;

            stockMovement.MovementValue = stockMovement.MovementType == MovementType.Compra ? ((stockMovement.UnitaryValue * stockMovement.Quantity) * -1) : (stockMovement.SalePrice * stockMovement.Quantity);

            _context.StockMovement.Add(stockMovement);
            await _context.SaveChangesAsync();

            //await _productService.UpdateBalanceStockAsync(stockMovement.ProductId, stockMovement.Quantity, stockMovement.MovementType);

            Product product = _context.Product.FirstOrDefault(p => p.Id == stockMovement.ProductId);


            if (stockMovement.MovementType == MovementType.Compra)
            {
                product.BalanceStock += stockMovement.Quantity;
            }
            else
            {
                product.BalanceStock -= stockMovement.Quantity;
            }

            await _productService.UpdateAsync(product);

            await _financialManagerService.UpdateAsync(stockMovement.ProductId);
        }

        public async Task DeleteAsync(StockMovement stockMovement)
        {
            _context.StockMovement.Remove(stockMovement);
            await _context.SaveChangesAsync();

            //await _productService.UpdateBalanceStockAsync(stockMovement.ProductId, (stockMovement.Quantity * -1), stockMovement.MovementType);

            Product product = _context.Product.FirstOrDefault(p => p.Id == stockMovement.ProductId);

            if (stockMovement.MovementType == MovementType.Compra)
            {
                product.BalanceStock -= stockMovement.Quantity;
            }
            else
            {
                product.BalanceStock += stockMovement.Quantity;
            }

            await _productService.UpdateAsync(product);

            await _financialManagerService.UpdateAsync(stockMovement.ProductId);
        }
    }
}

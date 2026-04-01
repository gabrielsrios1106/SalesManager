using DataTransferObjects.FinancialManager;
using DataTransferObjects.Products;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Services
{
    public class FinancialManagerService : IFinancialManagerService
    {
        private readonly SalesManagerContext _context;

        public FinancialManagerService(SalesManagerContext context)
        {
            _context = context;
        }

        public async Task<List<FinancialManagerGetDTO>> GetFinancialManagersAsync(int idUser)
        {
            IQueryable<FinancialManager> queryable = _context.FinancialManager
                                                             .AsNoTracking()
                                                             .Include(fm => fm.Product)
                                                             .ThenInclude(p => p.Department).AsSplitQuery()
                                                             .AsSplitQuery()
                                                             .Where(c => c.UserId == idUser);

            List<FinancialManagerGetDTO> financialManagersGetDTO = await queryable.Select(fm => new FinancialManagerGetDTO()
            {
                Id = fm.Id,
                GainSalesOfProduct = fm.GainSalesOfProduct,
                LossOrExpenseOfProduct = fm.LossOrExpenseOfProduct,
                ProfitOfProduct = fm.ProfitOfProduct,
                ProductId = fm.ProductId,
                ProductName = fm.Product.ProductName,
                Status = fm.Product.Status,
                DepartmentName = fm.Product.Department.DepartmentName

            }).ToListAsync();

            return financialManagersGetDTO;
        }

        public async Task<FinancialManager> GetFinancialManagerById(int productId) => await _context.FinancialManager.AsNoTracking().FirstOrDefaultAsync(fm => fm.ProductId == productId);

        public async Task<BalanceGetDTO> GetBalanceAsync(int idUser)
        {
            double gainSalesOfProduct = await _context.FinancialManager.Where(fm => fm.UserId == idUser).SumAsync(fm => fm.GainSalesOfProduct);
            double lossOrExpenseOfProduct = await _context.FinancialManager.Where(c => c.UserId == idUser).SumAsync(fm => fm.LossOrExpenseOfProduct);

            BalanceGetDTO balanceGetDTO = new BalanceGetDTO()
            {
                AllGain = gainSalesOfProduct,
                AllExpenseOrLoss = lossOrExpenseOfProduct,
                AllProfit = gainSalesOfProduct + lossOrExpenseOfProduct
            };

            return balanceGetDTO;
        }

        public async Task InsertAsync(int productId, int userId)
        {
            FinancialManager financialManager = new FinancialManager(){ ProductId = productId, UserId = userId };

            _context.FinancialManager.Add(financialManager);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int productId)
        {
            try
            {
                FinancialManager financialManager = await _context.FinancialManager.FirstOrDefaultAsync(fm => fm.ProductId == productId);

                financialManager.GainSalesOfProduct = await _context.StockMovement.Where(sm => sm.ProductId == productId && sm.MovementType == MovementType.venda).SumAsync(sm => sm.MovementValue);
                financialManager.LossOrExpenseOfProduct = await _context.StockMovement.Where(sm => sm.ProductId == productId && sm.MovementType == MovementType.Compra).SumAsync(sm => sm.MovementValue);

                financialManager.ProfitOfProduct = financialManager.GainSalesOfProduct + financialManager.LossOrExpenseOfProduct;

                _context.Entry(financialManager).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException($"Erro ao atualizar: {e.Message}");
            }
        }

        public async Task DeleteAsync(FinancialManager financialManager)
        {
            _context.FinancialManager.Remove(financialManager);
            await _context.SaveChangesAsync();
        }
    }
}

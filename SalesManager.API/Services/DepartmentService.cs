using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Data;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly SalesManagerContext _context;

        public DepartmentService(SalesManagerContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetDepartmentsAsync(string searchValue, int idUser, bool showInactive)
        {
            IQueryable<Department> queryable = _context.Department
                                                       .AsNoTracking()
                                                       .AsSplitQuery()
                                                       .Where(c => c.UserId == idUser)
                                                       .OrderByDescending(d => d.CreatedAt);

            if (!showInactive)
            {
                queryable = queryable.Where(d => d.Status == 1);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                queryable = queryable.Where(d => d.DepartmentName.ToLower().Contains(searchValue.ToLower()));
            }

            //List<DepartmentGetDTO> departmentsGetDTO = await queryable.Select(p => new DepartmentGetDTO()
            //{
            //    Id = p.Id,
            //    DepartmentName = p.DepartmentName,
            //    Status = p.Status
            //}).ToListAsync();

            return await queryable.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Department
                                 .AsNoTracking()
                                 .AsSplitQuery()
                                 .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task InsertAsync(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            try
            {
                _context.Entry(department).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException($"Erro ao atualizar: {e.Message}");
            }
        }

        public async Task DeleteAsync(Department department)
        {
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int departmentId, int idUser) => await _context.Department.AsNoTracking().AnyAsync(d => d.Id == departmentId && d.UserId == idUser);

        public async Task<bool> ExistsByNameAsync(string departmentName, int idUser) => await _context.Department.AsNoTracking().AnyAsync(d => d.DepartmentName.ToLower() == departmentName.ToLower() && d.UserId == idUser);

        public async Task<bool> ExistsByNameUpdateAsync(string departmentName, int departmentId, int idUser) => await _context.Department.AsNoTracking().AnyAsync(d => d.DepartmentName.ToLower() == departmentName.ToLower() && d.Id != departmentId && d.UserId == idUser);

        public async Task<bool> HasProduct(int departmentId, int idUser) => await _context.Product.AsNoTracking().AnyAsync(a => a.DepartmentId == departmentId && a.UserId == idUser);
    }
}

using Models;

namespace SalesManager.API.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetDepartmentsAsync(string searchValue, int idUser, bool showInactive);

        Task<Department> GetDepartmentByIdAsync(int departmentId);

        Task InsertAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);

        Task<bool> ExistsAsync(int departmentId, int idUser);

        Task<bool> ExistsByNameAsync(string departmentName, int idUser);

        Task<bool> ExistsByNameUpdateAsync(string departmentName, int departmentId, int idUser);

        Task<bool> HasProduct(int departmentId, int idUser);
    }
}

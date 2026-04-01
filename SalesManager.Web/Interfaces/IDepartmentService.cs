using DataTransferObjects.Departments;

namespace SalesManager.Web.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentGetDTO>> GetDepartmentsAsync(string requestUri);

        Task<DepartmentGetDTO> GetDepartmentByIdAsync(string requestUri);

        Task<bool> CreateAsync(string requestUri, DepartmentPostDTO departmentPostDTO);

        Task<bool> UpdateAsync(string requestUri, DepartmentPutDTO departmentPutDTO);

        Task<bool> DeleteAsync(string requestUri);
    }
}

using DataTransferObjects.Departments;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IAPIService _apiService;

        public DepartmentService(IAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<DepartmentGetDTO>> GetDepartmentsAsync(string requestUri)
        {
            return await _apiService.GetListAsync<DepartmentGetDTO>($"v1/{requestUri}");
        }

        public async Task<DepartmentGetDTO> GetDepartmentByIdAsync(string requestUri)
        {
            return await _apiService.GetByIdAsync<DepartmentGetDTO>($"v1/{requestUri}");
        }

        public async Task<bool> CreateAsync(string requestUri, DepartmentPostDTO departmentPostDTO)
        {
            return await _apiService.CreateAsync($"v1/{requestUri}", departmentPostDTO);
        }

        public async Task<bool> UpdateAsync(string requestUri, DepartmentPutDTO departmentPutDTO)
        {
            return await _apiService.UpdateAsync($"v1/{requestUri}", departmentPutDTO);
        }

        public async Task<bool> DeleteAsync(string requestUri)
        {
            return await _apiService.DeleteAsync($"v1/{requestUri}");
        }
    }
}

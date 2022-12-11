using BackendEmployeeAPI.Models;

namespace BackendEmployeeAPI.Services.Contract
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartments();
    }
}

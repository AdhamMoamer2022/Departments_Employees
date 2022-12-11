using BackendEmployeeAPI.Models;

namespace BackendEmployeeAPI.Services.Contract
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployees();
        public Task<Employee> GetEmployee(int id);
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<bool> DeleteEmployee(Employee employee);
    }
}

using BackendEmployeeAPI.Models;
using BackendEmployeeAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BackendEmployeeAPI.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DbemployeeContext _context;

        public EmployeeService(DbemployeeContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                employees = await _context.Employees.Include(dep => dep.IdDepartmentNavigation).ToListAsync();
                return employees;
            }
            catch (Exception e) { throw e; }

        }

        public async Task<Employee> GetEmployee(int id)
        {
            try
            {
                Employee? employee = new Employee();
                employee = await _context.Employees.Include(dep => dep.IdDepartmentNavigation)
                     .Where(emp => emp.IdEmployee == id).FirstOrDefaultAsync();
                return employee;
            }
            catch (Exception e) { throw e; }

        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return employee;

            }
            catch (Exception e) { throw e; }
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception e) { throw e; }
        }
        public async Task<bool> DeleteEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e) { throw e; }
        }


    }
}

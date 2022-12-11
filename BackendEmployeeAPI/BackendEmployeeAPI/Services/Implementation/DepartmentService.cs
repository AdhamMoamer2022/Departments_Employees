using BackendEmployeeAPI.Models;
using BackendEmployeeAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BackendEmployeeAPI.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DbemployeeContext _context;

        public DepartmentService(DbemployeeContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> GetDepartments()
        {
            try
            {
                List<Department> departments = new List<Department>();
                departments = await _context.Departments.ToListAsync();

                return departments;
            }
            catch (Exception e) { throw e; }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Synel_staff.Application.EmployeeInterfaces;
using Synel_staff.Domain.Entities;
using Synel_staff.Infrastructure.Persistance;

namespace Synel_staff.Infrastructure.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepo(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext; 
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var allEmp = await _appDbContext.Employees.AsNoTracking().ToListAsync();

            return allEmp;
        }

        public async Task<int> WriteRecordsAsync(IEnumerable<Employee> empRecords)
        {
            await _appDbContext.Employees.AddRangeAsync(empRecords);
            var totalRecords = await _appDbContext.SaveChangesAsync();          

            return totalRecords;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _appDbContext.Employees.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Employee emp)
        {
            _appDbContext.Employees.Update(emp);
            var row = await _appDbContext.SaveChangesAsync();
            
            return row > 0;
        }

    }
}

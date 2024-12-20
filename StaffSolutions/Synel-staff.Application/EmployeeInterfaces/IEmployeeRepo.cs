
using Synel_staff.Domain.Entities;

namespace Synel_staff.Application.EmployeeInterfaces
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<int> WriteRecordsAsync(IEnumerable<Employee> empRecords);
        Task<Employee> GetEmployeeAsync(int id);
        Task<bool> UpdateAsync(Employee emp);
    }
}

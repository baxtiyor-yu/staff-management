using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synel_staff.Domain.Entities;

namespace Synel_staff.Application.EmployeeInterfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<int?> WriteRecordsAsync(Stream csvFileStream);
        Task<Employee> GetEmployeeAsync(int id);
        Task<bool> UpdateAsync(Employee emp);
    }

}

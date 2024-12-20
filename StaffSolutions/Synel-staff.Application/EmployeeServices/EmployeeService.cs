

using System.Diagnostics.Metrics;
using Synel_staff.Application.Common;
using Synel_staff.Application.EmployeeInterfaces;
using Synel_staff.Domain.Entities;

namespace Synel_staff.Application.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly CsvService _csvService;
        public EmployeeService(IEmployeeRepo employeeRepo, CsvService csvService) 
        {
            _employeeRepo = employeeRepo;
            _csvService = csvService;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepo.GetAllEmployeesAsync();
        }

        public async Task<int?> WriteRecordsAsync(Stream csvFileStream)
        {
            var empRrecords = _csvService.ReadCsvFile(csvFileStream);
            if (empRrecords == null) 
            {
                return null;
            }
            return await _employeeRepo.WriteRecordsAsync(empRrecords);
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _employeeRepo.GetEmployeeAsync(id);
        }

        public async Task<bool> UpdateAsync(Employee emp)
        {
            return await _employeeRepo.UpdateAsync(emp);
        }
    }
}

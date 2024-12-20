
using System.Net;
using System.Reflection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Synel_staff.Domain.Entities;
using Synel_staff.Infrastructure.Persistance;
using Synel_staff.Infrastructure.Repositories;

namespace Synel_staff.Tests.InfrastructureTest.Repositories
{
    public class EmployeeRepoTests
    {
        private readonly AppDbContext _appDbContext;
        private readonly EmployeeRepo _employeeRepo;
        public EmployeeRepoTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeDb").Options;  
            
            _appDbContext = new AppDbContext(options);
            _employeeRepo = new EmployeeRepo(_appDbContext);
        }

        [Fact]
        public async Task GetAllEmployees_WhenFound()
        {
            //Arrange
            var empList = new List<Employee>()
            {
                new Employee
                {
                    Id = 1,
                    PayrollNumber = "AAAA",
                    Forenames = "Forn",
                    Surname = "Surn",
                    DateOfBirth = new DateOnly(2024, 11, 11),
                    Telephone = "56565",
                    Mobile = "56565",
                    Address = "Addd",
                    Address2 = "Address22",
                    Postcode = "Postcodeee",
                    EMailHome = "Emailll",
                    StartDate = new DateOnly(2024, 10, 10),
                },
                new Employee
                {
                    Id = 2,
                    PayrollNumber = "BBBBB",
                    Forenames = "FornBB",
                    Surname = "SurnBB",
                    DateOfBirth = new DateOnly(2024, 09, 09),
                    Telephone = "56565bBB",
                    Mobile = "56565bb",
                    Address = "AdddBB",
                    Address2 = "Address22bBB",
                    Postcode = "PostcodeeeBB",
                    EMailHome = "EmailllBBB",
                    StartDate = new DateOnly(2024, 07, 08),
                }
            };

            _appDbContext.Employees.AddRange(empList);
            await _appDbContext.SaveChangesAsync();

            //Act
            var result = await _employeeRepo.GetAllEmployeesAsync();

            //Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetEmployee_WhenFound()
        {
            //Arrange
            int id = 1;
            
            //Act
            var result = await _employeeRepo.GetEmployeeAsync(id);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetEmployee_WhenNotFound()
        {
            //Arrange
            int id = 999;

            //Act
            var result = await _employeeRepo.GetEmployeeAsync(id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]  
        public async Task UpdateAsync_WhenSuccess()
        {
            //Arrange
            var emp = new Employee
            {
                Id = 2,
                PayrollNumber = "Updated",
                Forenames = "Updated",
                Surname = "Updated",
                DateOfBirth = new DateOnly(2024, 09, 09),
                Telephone = "Updated",
                Mobile = "56565bb",
                Address = "AdddBB",
                Address2 = "Address22bBB",
                Postcode = "PostcodeeeBB",
                EMailHome = "EmailllBBB",
                StartDate = new DateOnly(2024, 07, 08),
            };

            //Act
            var result = await _employeeRepo.UpdateAsync(emp);

            //Assert
            result.Should().BeTrue();
        }
    }
}


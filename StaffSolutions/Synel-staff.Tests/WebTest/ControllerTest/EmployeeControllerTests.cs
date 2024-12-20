
using System.Text;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Synel_staff.Application.EmployeeInterfaces;
using Synel_staff.Domain.Entities;
using Synel_staff.Web.Controllers;

namespace Synel_staff.Tests.WebTest.ControllerTest
{
    public class EmployeeControllerTests
    {
        private EmployeeController _employeeController;
        private IEmployeeService _employeeService;
        public EmployeeControllerTests()
        {
            _employeeService = A.Fake<IEmployeeService>();
            _employeeController = new EmployeeController(_employeeService);
        }

        [Fact]
        public void EmployeeController_Index_ReturnSuccess()
        {
            //Arrange
            var empList = A.Fake<List<Employee>>();
            A.CallTo(()=> _employeeService.GetAllEmployeesAsync()).Returns(empList);

            //Act
            var result =  _employeeController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void EmployeeController_Edit_ReturnSuccess()
        {
            //Arrange
            var id = 1;
            var emp = A.Fake<Employee>();
            A.CallTo(() => _employeeService.GetEmployeeAsync(id)).Returns(emp);

            //Act
            var result = _employeeController.Edit(id);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public async Task EmployeeController_Edit_WhenEmployeeDoesNotExist()
        {
            // Arrange
            int id = 1;
            A.CallTo(() => _employeeService.GetEmployeeAsync(id)).Returns(Task.FromResult<Employee>(null!));

            // Act
            var result = await _employeeController.Edit(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task EmployeeController_Update_RedirectsToIndex()
        {
            // Arrange
            var tempData = new TempDataDictionary(
            new DefaultHttpContext(),
            A.Fake<ITempDataProvider>());
            _employeeController.TempData = tempData;
            var emp = A.Fake<Employee>();
            A.CallTo(() => _employeeService.UpdateAsync(emp)).Returns(true);

            // Act
            var result = await _employeeController.Update(emp);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            _employeeController.TempData["success"].Should().Be(" Record updated successfully");
        }

        [Fact]
        public async Task EmployeeController_Update_RedirectsToError()
        {
            // Arrange
            var tempData = new TempDataDictionary(
            new DefaultHttpContext(),
            A.Fake<ITempDataProvider>());
            _employeeController.TempData = tempData;
            var emp = A.Fake<Employee>();
            A.CallTo(() => _employeeService.UpdateAsync(emp)).Returns(false);

            // Act
            var result = await _employeeController.Update(emp);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            _employeeController.TempData["error"].Should().Be(" An error occurred while saving the data.");
        }

        [Fact]
        public async Task ProcessCsvAsync_WithValidCsv_ShouldReturnLineCount()
        {
            // Arrange
            var tempData = new TempDataDictionary(
            new DefaultHttpContext(),
            A.Fake<ITempDataProvider>());
            _employeeController.TempData = tempData;
            var csvContent = "Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname,Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address,Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date\r\nCOOP08,John ,William,26/01/1955,12345678,987654231,12 Foreman road,London,GU12 6JW,nomadic20@hotmail.co.uk,18/04/2013\r\nJACK13,Jerry,Jackson,11/5/1974,2050508,6987457,115 Spinney Road,Luton,LU33DF,gerry.jackson@bt.com,18/04/2013";
            var csvBytes = Encoding.UTF8.GetBytes(csvContent);
            var csvStream = new MemoryStream(csvBytes);
            var fakeFile = A.Fake<IFormFile>();
            A.CallTo(() => fakeFile.OpenReadStream()).Returns(csvStream);
            A.CallTo(() => _employeeService.WriteRecordsAsync(csvStream)).Returns(2);

            //Act
            var result = await _employeeController.Index(fakeFile);

            //Assert
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = result as RedirectToActionResult;
            _employeeController.TempData["success"].Should().Be("2 records have been added");
        }
    }
}
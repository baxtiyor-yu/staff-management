using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Synel_staff.Application.EmployeeInterfaces;
using Synel_staff.Domain.Entities;
using Synel_staff.Web.Models;

namespace Synel_staff.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        
        public EmployeeController(IEmployeeService employeeService)
        {
            //Initialize IEmployeeService through Dependency Injection
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Get all employees from the database
            var empList = await _employeeService.GetAllEmployeesAsync();
            //Pass list of employees to the View so we can display the list.
            return View(empList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile csvFile)
        {
            //Passing csv file content as a Stream
            var totalRecords = await _employeeService.WriteRecordsAsync(csvFile.OpenReadStream());
            if (totalRecords == null)
            {
                return BadRequest("No data provided");
            }
            //Popular Toastr notification package is used
            TempData["success"] = totalRecords + " records have been added";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int Id)
        {
            //For editing we need to pull an emloyee record out of DB
            var emp = await _employeeService.GetEmployeeAsync(Id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        public async Task<IActionResult> Update(Employee emp)
        {
            //Passing amended employee record
            var res = await _employeeService.UpdateAsync(emp);
            if (!res)
            {
                TempData["error"] = " An error occurred while saving the data.";

                return RedirectToAction("Error");
            }

            TempData["success"] = " Record updated successfully";

            return RedirectToAction(nameof(Index)); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var ss = HttpContext.Features.Get<IExceptionHandlerPathFeature>(); 
            ViewBag.EMessage = ss?.Error.Message;
            TempData["error"] = $"{ss?.Error.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
}

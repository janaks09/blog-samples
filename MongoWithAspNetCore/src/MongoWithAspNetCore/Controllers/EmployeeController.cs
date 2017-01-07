using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoWithAspNetCore.Models;
using MongoWithAspNetCore.Repositories;

namespace MongoWithAspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository();
        }

        [HttpGet("")]
        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            return new OkObjectResult(employees);
        }

        [HttpGet("{empId}")]
        public IActionResult GetEmployeeById(int empId)
        {
            var employee = _employeeRepository.GetEmployeeById(empId);
            return new OkObjectResult(employee);
        }

        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
            return new OkObjectResult(employee);

        }


        [HttpPut("{empId}")]
        public IActionResult Put(int empId, [FromBody]Employee employee)
        {
            
            var searchedEmployee = _employeeRepository.GetEmployeeById(empId);
            if (searchedEmployee == null) return NotFound();

            //employee.Id = searchedEmployee.Id;
            _employeeRepository.UpdateEmployee(empId, employee);
            return new OkResult();
        }

        [HttpDelete("{empId}")]
        public IActionResult Delete(int empId)
        {
            var employee = _employeeRepository.GetEmployeeById(empId);
            if (employee == null) return NotFound();

            _employeeRepository.DeleteEmployee(empId);
            return new OkResult();
        }



    }
}

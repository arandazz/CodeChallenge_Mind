using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/employeeCompensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received salary create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getSalaryByEmployeeId", new { id = compensation.Employee.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "getSalaryByEmployeeId")]
        public IActionResult GetSalaryByEmployeeId(String id)
        {
            _logger.LogDebug($"Received employee salary get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
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
    [Route("api/employeeReports")]
    public class ReportingController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IReportingService _reportingService;

        public ReportingController(ILogger<ReportingController> logger, IEmployeeService employeeService, IReportingService reportingService)
        {
            _logger = logger;
            _reportingService = reportingService;
            _employeeService = employeeService;
        }

        [HttpGet("{id}", Name = "getDirectReportsByEmployeeId")]
        public IActionResult GetDirectReportsByEmployeeId(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            var numberOfDirectReports = _reportingService.GetNumberOfDirectReports(employee);
            var reportingStructure = Map(employee, numberOfDirectReports);

            return Ok(reportingStructure);
        }

        private ReportingStructure Map(Employee employee, int numberOfDirectReports)
        {
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = numberOfDirectReports
            };
        }
    }
}

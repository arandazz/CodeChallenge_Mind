using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;

namespace challenge.Services
{
    public class ReportingService : IReportingService
    {
        private readonly ILogger<ReportingService> _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingService(ILogger<ReportingService> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public int GetNumberOfDirectReports(Employee employee)
        {
            var updatedEmployee = _employeeService.GetById(employee.EmployeeId);
            var numberOfDirectReports = updatedEmployee.DirectReports?.Count ?? 0;
            if (updatedEmployee.DirectReports != null)
            {
                foreach (var underling in updatedEmployee.DirectReports)
                {
                    numberOfDirectReports += GetNumberOfDirectReports(underling);
                }
            }

            return numberOfDirectReports;
        }
    }
}

using challenge.Models;

namespace challenge.Services
{
    public interface IReportingService
    {
        int GetNumberOfDirectReports(Employee employee);
    }
}
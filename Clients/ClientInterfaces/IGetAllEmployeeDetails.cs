using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IGetAllEmployeeDetails
    {
        Task<List<EmployeeDetails>> Execute(CancellationToken cancellationToken = default);
        Func<string> RequestUri { get; }
    }
}

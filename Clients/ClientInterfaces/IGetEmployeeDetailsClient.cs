using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IGetEmployeeDetailsClient
    {
        Task<EmployeeDetails> Execute(EmployeeDetailsRequestModel request,CancellationToken cancellationToken = default);
        Func<EmployeeDetailsRequestModel,string> RequestUri { get; }
    }
}

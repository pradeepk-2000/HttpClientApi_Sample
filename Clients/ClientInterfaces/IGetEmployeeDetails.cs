using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IGetEmployeeDetails
    {
        Task<EmployeeDetails> Execute(EmployeeDetailsRequestModel request,CancellationToken cancellationToken = default);
        Func<EmployeeDetailsRequestModel,string> RequestUri { get; }
    }
}

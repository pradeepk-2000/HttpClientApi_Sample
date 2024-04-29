using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IAddNewEmployeeClient
    {
        Task<bool> Execute(NewEmployeeDetailsRequestModel request, CancellationToken cancellationToken=default);
        Func<NewEmployeeDetailsRequestModel,string> RequestUri { get; }
    }
}

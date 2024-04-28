using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IAddNewEmployee
    {
        Task<bool> Execute(NewEmployeeDetailsRequestModel request, CancellationToken cancellationToken=default);
        Func<NewEmployeeDetailsRequestModel,string> RequestUri { get; }
    }
}

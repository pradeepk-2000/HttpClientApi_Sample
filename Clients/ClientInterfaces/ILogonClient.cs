using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface ILogonClient
    {
        Task<bool> Execute(LogonClientRequestModel request, CancellationToken cancellationToken = default);

        Func<LogonClientRequestModel, string> RequestUri { get; }
    }
}

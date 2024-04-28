using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IGetBatchListClient
    {
        Task<BatchListResponseModel> Execute(BatchListRequestModel request, CancellationToken cancellationToken = default);

        Func<BatchListRequestModel, string> RequestUri { get; }
    }
}

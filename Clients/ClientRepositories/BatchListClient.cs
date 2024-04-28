using AutoMapper;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.BaseClient;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using Polly;

namespace PerfectAPI.Clients.ClientRepositories
{
    public class BatchListClient : BaseClient<BatchListRequestModel, BatchListResponseModel>, IGetBatchListClient
    {
        public BatchListClient(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<BatchListClient> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                                      : base(httpClient, environmentSettings, httpClientFactory, logger, mapper)//, policy)
        {
        }

        public override Func<BatchListRequestModel, string> RequestUri => x => $"Batches?Num={x.Id}&App={x.App}";

        internal override Task<BatchListResponseModel> CreateResult(HttpResponseMessage response)
        {
            //BatchListResponseModel result = response.Content.ReadFromJsonAsync<BatchListResponseModel>().Result;
            return Task.FromResult(
                Mapper.Map<BatchListResponseModel>(
                    response.Content.ReadFromJsonAsync<BatchListResponseModel>().Result
                    )
                );
        }

        internal override async Task<HttpResponseMessage> GetHttpResponse(BatchListRequestModel request)
        {
            return await Client.GetAsync(RequestUri(request));
        }
    }
}

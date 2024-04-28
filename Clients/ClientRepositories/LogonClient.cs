using AutoMapper;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.BaseClient;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using Polly;

namespace PerfectAPI.Clients.ClientRepositories
{
    public class LogonClient : BaseClient<LogonClientRequestModel, bool>, ILogonClient
    {
        public LogonClient(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<LogonClient> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                              : base(httpClient, environmentSettings, httpClientFactory, logger, mapper)//, policy)
        {
        }

        public override Func<LogonClientRequestModel, string> RequestUri => x => "Session/Logon";

        internal override Task<bool> CreateResult(HttpResponseMessage response)
        {
            return Task.FromResult(true);
        }

        internal override Task<HttpResponseMessage> GetHttpResponse(LogonClientRequestModel request)
        {
            return Client.PostAsJsonAsync(RequestUri(request), Mapper.Map<LogonClientRequestModel>(request));
        }

    }
}

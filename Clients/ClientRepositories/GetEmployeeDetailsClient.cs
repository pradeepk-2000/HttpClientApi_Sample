using AutoMapper;
using Newtonsoft.Json;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.BaseClient;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Helpers;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using Polly;
using System.Net.Http.Json;
using System.Text.Json;

namespace PerfectAPI.Clients.ClientRepositories
{

    public class GetEmployeeDetailsClient : BaseClient<EmployeeDetailsRequestModel,EmployeeDetails>, IGetEmployeeDetailsClient
    {

        public GetEmployeeDetailsClient(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<GetEmployeeDetailsClient> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                                           : base(httpClient, environmentSettings, httpClientFactory, logger, mapper)//, policy)
        {
        }

        public override Func<EmployeeDetailsRequestModel,string> RequestUri => x => $"GetEmployeeDetails?EmpId={x.EmpId}";

        internal override async Task<EmployeeDetails> CreateResult(HttpResponseMessage response)
        {
            //return Task.FromResult(
            //    Mapper.Map<EmployeeDetails>(
            //        response.Content.ReadFromJsonAsync<EmployeeDetails>().Result
            //        )
            //    );

            var jsonContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<EmployeeDetails>>(jsonContent);
            return apiResponse.Result;
        }

        internal override Task<HttpResponseMessage> GetHttpResponse(EmployeeDetailsRequestModel request)
        {
            return  Client.GetAsync(RequestUri(request));
        }
    }
}

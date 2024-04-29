using AutoMapper;
using Newtonsoft.Json;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.BaseClient;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Helpers;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using Polly;
using System.Net.Http;
using System.Text.Json;

namespace PerfectAPI.Clients.ClientRepositories
{
    
    public class GetAllEmployeeDetailsClient : BaseClient<List<EmployeeDetails>>, IGetAllEmployeeDetailsClient
    {

        public GetAllEmployeeDetailsClient(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<GetAllEmployeeDetailsClient> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                                                  : base(httpClient, environmentSettings, httpClientFactory, logger, mapper)//, policy)
        {
        }
 
        public override Func<string> RequestUri => () => "GetAllEmployeeDetails";

        internal override async  Task<List<EmployeeDetails>> CreateResult(HttpResponseMessage response)
        {
            //return Task.FromResult(
            //    Mapper.Map<List<EmployeeDetails>>(
            //        response.Content.ReadFromJsonAsync<List<EmployeeDetails>>()
            //        )
            //    ) ;

            var jsonContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<EmployeeDetails>>>(jsonContent);

            return apiResponse.Result;

        }

        internal override async Task<HttpResponseMessage> GetHttpResponse()
        {
            return await Client.GetAsync(RequestUri());
        }
    }
}

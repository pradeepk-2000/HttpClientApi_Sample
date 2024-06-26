﻿using AutoMapper;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.BaseClient;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using Polly;

namespace PerfectAPI.Clients.ClientRepositories
{
    public class AddNewEmployeeClient : BaseClient<NewEmployeeDetailsRequestModel, bool>, IAddNewEmployeeClient
    {

        public AddNewEmployeeClient(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<AddNewEmployeeClient> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                                   : base(httpClient, environmentSettings, httpClientFactory, logger, mapper)//, policy)
        {
        }

        public override Func<NewEmployeeDetailsRequestModel, string> RequestUri => x => "AddNewEmployee";

        internal override Task<bool> CreateResult(HttpResponseMessage response)
        {
            return Task.FromResult(true);
        }

        internal override Task<HttpResponseMessage> GetHttpResponse(NewEmployeeDetailsRequestModel request)
        {
            return Client.PostAsJsonAsync(RequestUri(request), Mapper.Map<NewEmployeeDetailsRequestModel>(request));
        }
    }
}

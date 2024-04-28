﻿using AutoMapper;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.BaseClient;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using Polly;

namespace PerfectAPI.Clients.ClientRepositories
{
    public class UpdateEmployeeDesignation : BaseClient<UpdateEmployeDesignationRequestModel,bool>, IUpdateEmployeeDesignation
    {

        public UpdateEmployeeDesignation(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<UpdateEmployeeDesignation> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                                                          : base(httpClient, environmentSettings, httpClientFactory, logger, mapper)//, policy)
        {
        }

        public override Func<UpdateEmployeDesignationRequestModel,string> RequestUri => x => "UpdateEmployeeDesignation";

        internal override Task<bool> CreateResult(HttpResponseMessage response)
        {
            return Task.FromResult(true);
        }

        internal override Task<HttpResponseMessage> GetHttpResponse(UpdateEmployeDesignationRequestModel request)
        {
            return Client.PostAsJsonAsync(RequestUri(request), Mapper.Map<UpdateEmployeDesignationRequestModel>(request));
        }
    }
}

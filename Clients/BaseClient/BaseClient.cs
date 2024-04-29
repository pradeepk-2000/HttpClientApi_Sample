using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PerfectAPI.Helpers;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using Polly;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PerfectAPI.Clients.BaseClient
{
    // Default BaseClient abstract class
    public abstract class BaseClient
    {
        public HttpClient Client { get; set; }
        public IHttpClientFactory HttpClientFactory { get; set; }
        public ILogger<BaseClient> Logger { get; set; }
        public IEnvironmentSettings EnvironmentSettings { get; set; }
        public IMapper Mapper { get; set; }
        //public IAsyncPolicy Policy { get; set; }
        protected Stopwatch CallTimer { get; set; } = new Stopwatch();

        protected BaseClient(HttpClient httpClient, IHttpClientFactory httpClientFactory,IEnvironmentSettings environmentSettings, ILogger<BaseClient> logger, IMapper mapper)//, IAsyncPolicy policy)
        {
            Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            EnvironmentSettings = environmentSettings ?? throw new ArgumentNullException(nameof(environmentSettings));
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
         //   Policy = policy ?? throw new ArgumentNullException(nameof(policy));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void SetHttpClient(HttpClient client, Uri uri)
        {
            this.Client = client;
            this.Client.BaseAddress = uri;
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //this.Client.DefaultRequestHeaders.Add("accept", "application/json");
           // this.Client.DefaultRequestHeaders.Add("Content-Type", "application/json"); only for post
        }

        public Uri GetBaseAddress(string env)
        {
            string baseAddress = string.Empty;
            switch (env)
            {
                case "DEV":
                    baseAddress =  Constants.CONTEXT_URI;
                    break;
                case "IT":
                    baseAddress = Environment.GetEnvironmentVariable("ITURL");
                    break;
                default:
                    baseAddress = Environment.GetEnvironmentVariable("DEVURL");
                    break;
            }
            return new Uri(baseAddress);
        }

    }

    // BaseClient class for only response
    public abstract class BaseClient<TResponse> : BaseClient
    {
        protected BaseClient(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<BaseClient> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                               : base(httpClient, httpClientFactory, environmentSettings,logger, mapper)//, policy)
        {
        }

        public abstract Func<string> RequestUri { get; }

        internal abstract Task<HttpResponseMessage> GetHttpResponse();

        internal abstract Task<TResponse> CreateResult(HttpResponseMessage response);

        public async Task<TResponse> Execute(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Logger.LogDebug("Executing [{RequestUri}]", RequestUri());

            var uri = this.GetBaseAddress(this.EnvironmentSettings.Environment);

            this.SetHttpClient(this.HttpClientFactory.CreateClient("perfectApi"), uri);
            // Make the call
            CallTimer.Start();

            HttpResponseMessage response;
            try
            {
                //response = await Policy.ExecuteAsync(() => this.GetHttpResponse());
                response = await  this.GetHttpResponse();
            }
            finally
            {
                CallTimer.Stop();
            }

            // Log elapsed time
            Logger.LogInformation("Elapsed [{elapsed:n}] ms", CallTimer.ElapsedMilliseconds);

            try
            {
                response.ValidateForErrors();
            }
            catch (Exception ex)
            {
                throw new WebTaskMasterException(response, ex);
            }

            if (response.IsResourceNotFound())
            {
                Logger.LogWarning("Not found [{@default}]", default(TResponse));
                return default;
            }

            var result = await this.CreateResult(response).ConfigureAwait(false);
            return result;
        }
    }

    // BaseClient for both request and response
    public abstract class BaseClient<TRequest, TResponse> : BaseClient
    {
        protected BaseClient(HttpClient httpClient, IEnvironmentSettings environmentSettings, IHttpClientFactory httpClientFactory, ILogger<BaseClient> logger, IMapper mapper)//, IAsyncPolicy policy)
                                                                                                                                                                               : base(httpClient, httpClientFactory, environmentSettings,logger, mapper)//, policy)
        {
        }

        public abstract Func<TRequest, string> RequestUri { get; }

        internal abstract Task<HttpResponseMessage> GetHttpResponse(TRequest request);

        internal abstract Task<TResponse> CreateResult(HttpResponseMessage response);

        public async Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Logger.LogDebug("Executing [{ResponseUri}]", RequestUri(request));

            var uri = this.GetBaseAddress(this.EnvironmentSettings.Environment);

            this.SetHttpClient(this.HttpClientFactory.CreateClient("perfectApi"), uri);
            // Make the call
            CallTimer.Start();

            HttpResponseMessage response;
            try
            {
                //response = await Policy.ExecuteAsync(() => this.GetHttpResponse(request));
                response = await this.GetHttpResponse(request);
            }
            finally
            {
                CallTimer.Stop();
            }

            // Log elapsed time
            Logger.LogInformation("Elapsed [{elapsed:n}] ms", CallTimer.ElapsedMilliseconds);

            try
            {
                response.ValidateForErrors();
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError("HttpRequestException occurred: " + ex.StackTrace);
            }

            if (response.IsResourceNotFound())
            {
                Logger.LogWarning("Not found [{@default}]", default(TResponse));
                return default;
            }

            var result = await this.CreateResult(response).ConfigureAwait(false);
            return result;
        }
    }
}


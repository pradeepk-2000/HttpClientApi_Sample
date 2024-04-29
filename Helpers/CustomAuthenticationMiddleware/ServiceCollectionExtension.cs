using PerfectAPI.BusinessLayer.Factories;
using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Clients.ClientRepositories;
using Polly;
using Polly.Timeout;

namespace PerfectAPI.Helpers.CustomAuthenticationMiddleware
{
    public static class ServiceCollectionExtension
    {
        public static void AddWebTaskMasterAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterPolicies();
            serviceCollection.RegisterOperationClient();
        }

        private static void RegisterPolicies(this IServiceCollection serviceCollection)
        {
            //create a polly policy wrapper using timeout and retry limits

            int webServiceCallRetryAttempts = 2;
            int webServiceCallTimeoutSeconds = 600; // 10mins
            var retryPolicy = Policy.Handle<TimeoutRejectedException>()
                .RetryAsync(
                webServiceCallRetryAttempts,
                (exception, retryCount, context) =>
                {
                    if (context.Contains("Logger") && context["Logger"] is Serilog.ILogger logger)
                    {
                        logger.Warning(exception, "Retrying (attempt [{retryCount}] of [{@WebServiceCallRetryAttempts}]",
                            retryCount, webServiceCallRetryAttempts);
                    }
                });

            var timeoutPolicy = Policy.TimeoutAsync(webServiceCallTimeoutSeconds);

            var policyWrap = Policy.WrapAsync(retryPolicy,timeoutPolicy);

            serviceCollection.AddSingleton(typeof(IAsyncPolicy),policyWrap);
        }

        private static void RegisterOperationClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEnvironmentSettings, EnvironmentSettings>();

            serviceCollection.AddTransient<IEmployeeFactory, EmployeeFactory>();
            serviceCollection.AddTransient<IPerfectAPIClientFactory,PerfectAPICLientFactory>();

            //serviceCollection.AddTransient<IAddNewEmployeeClient, AddNewEmployeeClient>();
            //serviceCollection.AddTransient<IGetAllEmployeeDetailsClient, GetAllEmployeeDetailsClient>();
            //serviceCollection.AddTransient<IGetEmployeeDetailsClient, GetEmployeeDetailsClient>();
            //serviceCollection.AddTransient<IUpdateEmployeeDesignationClient, UpdateEmployeeDesignationClient>();

            serviceCollection.AddHttpClient<IAddNewEmployeeClient,AddNewEmployeeClient>();
            serviceCollection.AddHttpClient<IGetAllEmployeeDetailsClient, GetAllEmployeeDetailsClient>();
            serviceCollection.AddHttpClient<IGetEmployeeDetailsClient, GetEmployeeDetailsClient>();
            serviceCollection.AddHttpClient<IUpdateEmployeeDesignationClient, UpdateEmployeeDesignationClient>();
        }
    }
}

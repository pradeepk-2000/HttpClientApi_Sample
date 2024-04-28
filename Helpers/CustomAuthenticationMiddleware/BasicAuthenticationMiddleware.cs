namespace PerfectAPI.Helpers.CustomAuthenticationMiddleware
{
    // this class is only to set environment getting from request header for each request
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IEnvironmentSettings _environmentSettings;

        public BasicAuthenticationMiddleware(RequestDelegate requestDelegate, IEnvironmentSettings environmentSettings)
        {
            _requestDelegate = requestDelegate;
            _environmentSettings = environmentSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headers = context.Request.Headers;
            string? env = headers != null && headers.ContainsKey("X-Environment") ? Convert.ToString(headers["X-Environment"]) : null;

            if (!string.IsNullOrWhiteSpace(env))
            {
                _environmentSettings.Environment = "DEV";
                //headers.Add();
            }
            _environmentSettings.Environment = "DEV";

            await _requestDelegate(context);
           // return;//
        }

    }

    public static class BasicAuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder useBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}

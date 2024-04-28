using System.Net;
using System.Net;
using System.Net.Http;
namespace PerfectAPI.Helpers
{

    public static class HttpResponseMessageExtensions
    {
        public static bool IsResourceNotFound(this HttpResponseMessage response)
        {
            return response.StatusCode == HttpStatusCode.NotFound;
        }

        public static void ValidateForErrors(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}: {response.ReasonPhrase} during {response.RequestMessage.Method.Method}");
            }
            return;
        }

}
}

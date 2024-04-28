namespace PerfectAPI.Helpers
{
    public class WebTaskMasterException :HttpRequestException
    {

        public int StatusCode { get; set; }
        public HttpContent Content { get; set; }
        //public WebTaskMasterException(HttpResponseMessage response, Exception innerException) :base(response.InnerMessage().Result,innerException)
        //{
        //        StatusCode = (int) response.StatusCode;
        //        Content = response.Content;
        //}

        public WebTaskMasterException(HttpResponseMessage response, Exception innerException) : base("Error in web task master", innerException)
        {
            StatusCode = (int)response.StatusCode;
            Content = response.Content; // Synchronously read the content, consider using async/await instead
        }

    }
}

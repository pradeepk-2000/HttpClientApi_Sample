namespace PerfectAPI.Helpers
{
    public class ApiResponse<T> where T : class
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
}

namespace PerfectAPI.BusinessLayer.Models
{
    public class BatchListRequestModel
    {
        public int Id { get; set; }
        public string App { get; set; }
    }

    public class BatchListResponseModel
    {
        public int Count { get; set; }
    }

}

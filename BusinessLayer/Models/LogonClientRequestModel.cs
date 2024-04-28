using Destructurama.Attributed;

namespace PerfectAPI.BusinessLayer.Models
{
    public class LogonClientRequestModel
    {
        [LogMasked(Text = "***")]
        public string Password { get; set; }

        public string User { get; set; }
    }
}

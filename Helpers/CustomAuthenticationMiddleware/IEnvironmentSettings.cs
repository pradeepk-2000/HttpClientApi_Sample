namespace PerfectAPI.Helpers.CustomAuthenticationMiddleware
{
    public interface IEnvironmentSettings
    {
        public string Environment {  get; set; }    
    }

    public class EnvironmentSettings : IEnvironmentSettings
    {
        public string Environment { get ; set; }
    }
}

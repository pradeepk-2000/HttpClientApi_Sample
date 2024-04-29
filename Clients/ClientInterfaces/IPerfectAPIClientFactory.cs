namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IPerfectAPIClientFactory
    {
        IAddNewEmployeeClient AddNewEmployeeClient { get; }
        IGetAllEmployeeDetailsClient GetAllEmployeeDetailsClient { get; }
        IGetEmployeeDetailsClient GetEmployeeDetailsClient { get; }
        IUpdateEmployeeDesignationClient UpdateEmployeeDesignationClient { get; }

        // dummy api for testing
        //IGetBatchListClient GetBatchListClient { get; }
        //ILogonClient LogonClient { get; }
    }
}

using PerfectAPI.BusinessLayer.Factories;
using PerfectAPI.Clients.ClientInterfaces;

namespace PerfectAPI.Clients.ClientRepositories
{
    public class PerfectAPICLientFactory : IPerfectAPIClientFactory
    {
        public IAddNewEmployeeClient AddNewEmployeeClient { get; }

        public IGetAllEmployeeDetailsClient GetAllEmployeeDetailsClient { get; }

        public IGetEmployeeDetailsClient GetEmployeeDetailsClient { get; }

        public IUpdateEmployeeDesignationClient UpdateEmployeeDesignationClient { get; }

        //public IGetBatchListClient GetBatchListClient { get; }

        //public ILogonClient LogonClient { get; }

        
            public PerfectAPICLientFactory(IAddNewEmployeeClient addNewEmployee, IUpdateEmployeeDesignationClient updateEmployeeDesignation, IGetAllEmployeeDetailsClient getAllEmployeeDetails, IGetEmployeeDetailsClient getEmployeeDetails)
            {
                AddNewEmployeeClient = addNewEmployee;
                GetAllEmployeeDetailsClient = getAllEmployeeDetails;
                GetEmployeeDetailsClient = getEmployeeDetails;
                UpdateEmployeeDesignationClient = updateEmployeeDesignation;
            }
        
    }
}

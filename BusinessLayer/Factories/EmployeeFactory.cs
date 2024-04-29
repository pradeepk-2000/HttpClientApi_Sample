using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.ClientInterfaces;

namespace PerfectAPI.BusinessLayer.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IPerfectAPIClientFactory _perfectAPIClientFactory;

        public EmployeeFactory(IPerfectAPIClientFactory perfectAPIClientFactory)
        {
                _perfectAPIClientFactory = perfectAPIClientFactory;
        }
        public async Task<bool> AddNewEmployee(NewEmployeeDetailsRequestModel model)
        {
           return await _perfectAPIClientFactory.AddNewEmployeeClient.Execute(model);
        }

        public async Task<List<EmployeeDetails>> GetAllEmployeeDetails()
        {
            return await _perfectAPIClientFactory.GetAllEmployeeDetailsClient.Execute();
        }

        public async Task<EmployeeDetails?> GetEmployeeDetails(EmployeeDetailsRequestModel model)
        {
            return await _perfectAPIClientFactory.GetEmployeeDetailsClient.Execute(model);
        }

        public Task<bool> UpdateDesignation(UpdateEmployeDesignationRequestModel model)
        {
            return _perfectAPIClientFactory.UpdateEmployeeDesignationClient.Execute(model);   
        }
    }
}

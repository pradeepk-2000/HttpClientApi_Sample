using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Clients.ClientInterfaces;

namespace PerfectAPI.BusinessLayer.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IAddNewEmployee _addNewEmployee;
        private readonly IUpdateEmployeeDesignation _updateEmployeeDesignation;
        private readonly IGetAllEmployeeDetails _getAllEmployeeDetails;
        private readonly IGetEmployeeDetails _getEmployeeDetails;

        public EmployeeFactory(IAddNewEmployee addNewEmployee, IUpdateEmployeeDesignation updateEmployeeDesignation, IGetAllEmployeeDetails getAllEmployeeDetails, IGetEmployeeDetails getEmployeeDetails)
        {
             _addNewEmployee = addNewEmployee;
            _getAllEmployeeDetails = getAllEmployeeDetails;
            _getEmployeeDetails = getEmployeeDetails;
            _updateEmployeeDesignation = updateEmployeeDesignation;
        }
        public async Task<bool> AddNewEmployee(NewEmployeeDetailsRequestModel model)
        {
           return await  _addNewEmployee.Execute(model);
        }

        public async Task<List<EmployeeDetails>> GetAllEmployeeDetails()
        {
            return await _getAllEmployeeDetails.Execute();
        }

        public async Task<EmployeeDetails?> GetEmployeeDetails(EmployeeDetailsRequestModel model)
        {
            return await _getEmployeeDetails.Execute(model);
        }

        public Task<bool> UpdateDesignation(UpdateEmployeDesignationRequestModel model)
        {
            return _updateEmployeeDesignation.Execute(model);   
        }
    }
}

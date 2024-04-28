using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.BusinessLayer.Interfaces
{
    public interface IEmployeeFactory
    {
        Task<EmployeeDetails?> GetEmployeeDetails(EmployeeDetailsRequestModel model);
       Task<List<EmployeeDetails>> GetAllEmployeeDetails();
        Task<bool> UpdateDesignation(UpdateEmployeDesignationRequestModel model);
        Task<bool> AddNewEmployee(NewEmployeeDetailsRequestModel model);
    }
}

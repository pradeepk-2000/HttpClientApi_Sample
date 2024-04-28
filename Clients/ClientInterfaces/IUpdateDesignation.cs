using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IUpdateEmployeeDesignation
    {
        Task<bool> Execute(UpdateEmployeDesignationRequestModel request, CancellationToken cancellationToken= default);
        Func<UpdateEmployeDesignationRequestModel,string> RequestUri { get; }
    }
}

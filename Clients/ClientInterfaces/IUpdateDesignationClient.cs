using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Clients.ClientInterfaces
{
    public interface IUpdateEmployeeDesignationClient
    {
        Task<bool> Execute(UpdateEmployeDesignationRequestModel request, CancellationToken cancellationToken= default);
        Func<UpdateEmployeDesignationRequestModel,string> RequestUri { get; }
    }
}

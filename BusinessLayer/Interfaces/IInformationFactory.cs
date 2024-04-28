using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.BusinessLayer.Interfaces
{
    public interface IInformationFactory
    {
        Task<StatusResponseModel> CheckStatus(StatusRequestModel model);
    }
}

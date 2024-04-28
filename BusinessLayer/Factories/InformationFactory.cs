using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.BusinessLayer.Factories
{
    public class InformationFactory : IInformationFactory
    {
        public async Task<StatusResponseModel> CheckStatus(StatusRequestModel model)
        {
            return new StatusResponseModel
            {
                Status = true
            };

            // throw new NotImplementedException();
        }
    }
}

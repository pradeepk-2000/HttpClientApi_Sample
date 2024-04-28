using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectAPI.Helpers;
using System.Net;
using Serilog;
using Microsoft.Extensions.Logging;
using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.BusinessLayer.Models;

namespace PerfectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Home1Controller : ControllerBase
    {
        private readonly ILogger<Home1Controller> _logger;
        private readonly IInformationFactory _informationFactory;
        public Home1Controller(ILogger<Home1Controller> logger, IInformationFactory informationFactory)
        {

            _logger = logger;
            _informationFactory = informationFactory;

        }
        [HttpPost("statusCheck")]
        public async Task<IActionResult> CheckStatus([FromBody] StatusRequestModel model)
        {
            ServiceResponse<StatusResponseModel> serviceResponse;
            try
            {
                if (ModelState.IsValid)
                {
                    StatusResponseModel response = await _informationFactory.CheckStatus(model);
                    if (response.Status)
                    {
                        serviceResponse = new ServiceResponse<StatusResponseModel>
                        {
                            IsSuccess = true,
                            StatusCode = HttpStatusCode.OK,
                            Result = response
                        };

                        _logger.LogInformation($"CheckStatus API is Successfull with {model.StatusId}", serviceResponse.IsSuccess);
                        return Ok(serviceResponse);
                    }

                    serviceResponse = new ServiceResponse<StatusResponseModel>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                    };

                    _logger.LogInformation($"CheckStatus API is Notfound with {model.StatusId}",serviceResponse.IsSuccess);
                    return NotFound(serviceResponse);
                }
                else
                {
                    serviceResponse = new ServiceResponse<StatusResponseModel>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                    };

                    _logger.LogInformation("CheckStatus API model is Invalid", serviceResponse.IsSuccess);
                    return BadRequest(serviceResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal Server error" + ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, model);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Helpers;
using System.Net;

namespace PerfectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IEmployeeFactory employeeFactory, ILogger<HomeController> logger)
        {
            _employeeFactory = employeeFactory;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult HealthStatus()
        {
            return new JsonResult(true);
        }

        [HttpGet("GetAllEmployeeDetails")]
        public async Task<IActionResult> GetAllEmployeeDetails()
        {
            ServiceResponse<List<EmployeeDetails>> serviceResponse;
            try
            {
                var response = await _employeeFactory.GetAllEmployeeDetails();
                if(response!= null)
                {
                    serviceResponse = new ServiceResponse<List<EmployeeDetails>>
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        Result = response
                    };
                    return Ok(serviceResponse);
                }
                serviceResponse = new ServiceResponse<List<EmployeeDetails>>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                };
                return NotFound(serviceResponse);
            }
            catch (Exception ex) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }    
        }

        [HttpPost("AddNewEmployee")]
        public async Task<IActionResult> AddNewEmployee(NewEmployeeDetailsRequestModel model)
        {
            ServiceResponse<object> serviceResponse;
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _employeeFactory.AddNewEmployee(model);
                    if (response != null && response)
                    {
                        serviceResponse = new ServiceResponse<object>
                        {
                            StatusCode = HttpStatusCode.OK,
                            IsSuccess = true,
                            Result = response
                        };
                        return Ok(serviceResponse);
                    }
                    serviceResponse = new ServiceResponse<object>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false,
                    };
                    return BadRequest(serviceResponse);
                }
                serviceResponse = new ServiceResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                };
                return BadRequest(serviceResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("UpdateEmployeeDesignation")]
        public async Task<IActionResult> UpdateEmployeeDesignation([FromBody] UpdateEmployeDesignationRequestModel model)
        {
            ServiceResponse<object> serviceResponse;
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _employeeFactory.UpdateDesignation(model);
                    if (response)
                    {
                        serviceResponse = new ServiceResponse<object>
                        {
                            IsSuccess = true,
                            StatusCode = HttpStatusCode.OK,
                            Result = response
                        };

                       // _logger.LogInformation($"UpdateEmployeeDesignation API is Successfull", serviceResponse.IsSuccess);
                        return Ok(serviceResponse);
                    }

                    serviceResponse = new ServiceResponse<object>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                    };

                    //_logger.LogInformation($"UpdateEmployeeDesignation API is Notfound", serviceResponse.IsSuccess);
                    return NotFound(serviceResponse);
                }
                else
                {
                    serviceResponse = new ServiceResponse<object>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                    };

                   // _logger.LogInformation("UpdateEmployeeDesignation API model is Invalid", serviceResponse.IsSuccess);
                    return BadRequest(serviceResponse);
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Internal Server error" + ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, model);
            }

        }


        [HttpGet("GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployeeDetails([FromQuery] EmployeeDetailsRequestModel model)
        {
            ServiceResponse<EmployeeDetails> serviceResponse;
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _employeeFactory.GetEmployeeDetails(model);
                    if (response != null && !string.IsNullOrEmpty(response.EmpName))
                    {
                        serviceResponse = new ServiceResponse<EmployeeDetails>
                        {
                            IsSuccess = true,
                            StatusCode = HttpStatusCode.OK,
                            Result = response
                        };

                       // _logger.LogInformation($"GetEmployeeDetails API is Successfull", serviceResponse.IsSuccess);
                        return Ok(serviceResponse);
                    }

                    serviceResponse = new ServiceResponse<EmployeeDetails>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                    };

                   // _logger.LogInformation($"GetEmployeeDetails API is Notfound", serviceResponse.IsSuccess);
                    return NotFound(serviceResponse);
                }
                else
                {
                    serviceResponse = new ServiceResponse<EmployeeDetails>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                    };

                   // _logger.LogInformation("GetEmployeeDetails API model is Invalid", serviceResponse.IsSuccess);
                    return BadRequest(serviceResponse);
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Internal Server error" + ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, model);
            }

        }

    }
}

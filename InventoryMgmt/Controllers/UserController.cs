using InventoryMgmt.Application.DTOs;
using InventoryMgmt.Application.Enums;
using InventoryMgmt.Application.Interfaces.IRepositories;
using InventoryMgmt.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ILoginRepository _loginRepository;

        public UserController(ILogger<UserController> logger, ILoginRepository loginRepository)
        {
            _logger = logger;
            _loginRepository = loginRepository;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            ApiResponseInfo responseInfo = new ApiResponseInfo();
            _logger.LogInformation("Login api started...");
            try
            {   
                if (ModelState.IsValid)
                {
                    var response = await _loginRepository.ValidateUser(request);
                    if (String.IsNullOrEmpty(response.ErrorMessage))
                    {
                        responseInfo.Info.IsSuccess = true;
                        responseInfo.Info.Message = "Success";
                        responseInfo.Info.Code = StatusCodeEnum.Success;
                        responseInfo.Data = new
                        {
                            Email = response.Email
                        };
                        _logger.LogInformation("Login api success.");
                        return Ok(responseInfo);
                    }
                    else
                    {
                        responseInfo.Info.IsSuccess = false;
                        responseInfo.Info.Message = response.ErrorMessage;
                        responseInfo.Info.Code = StatusCodeEnum.BadRequest;
                        responseInfo.Data = null;
                        _logger.LogInformation("Login api failed due to user information ...");
                        return BadRequest(responseInfo);
                    }

                    
                }
                else
                {

                    return BadRequest(ModelState);
                }
              
            }

            catch(Exception ex)
            {
                responseInfo.Info.IsSuccess = false;
                responseInfo.Info.Message = ex.Message;
                responseInfo.Info.Code = StatusCodeEnum.InternalServerError;
                responseInfo.Data = null;
                _logger.LogInformation("Login api  throwing exception:"+ ex.Message );
                return Problem();
            }
        }
    }
}

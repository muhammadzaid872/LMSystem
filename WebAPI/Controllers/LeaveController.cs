using Core;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;
using WebAPI.Auth.Interface;

namespace WebAPI.Controllers
{
    public class LeaveController : APIControllerBase
    {
        private readonly ILeaveService _leaveService;
        private readonly ILogger<LeaveController> _logger;
        private readonly IAuthService _authService;
        public LeaveController(ILogger<LeaveController> logger,
                ILeaveService leaveService,
                 ILoggerService loggerService,
                IAuthService authService
                            ) : base(logger, loggerService)
        {
            _logger = logger;
            _leaveService = leaveService;
            _authService = authService;
        }
        [Authorize]
        [HttpGet("Leave/GetAll")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLeaves()
        {
            try
            {
                var response = new ResponseDTO();
                var loginUser = _authService.GetLoginUserInformation();
                if (loginUser.Success)
                {
                    var user = (UserDTO)loginUser.Data;
                    response = await _leaveService.GetAllLeaves(user.Id);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }
        [Authorize]
        [HttpPost("Leave/ApplyLeave")]
        [ProducesResponseType(typeof(LeaveRequestDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveRequestDTO request)
        {
            try
            {
                var response = new ResponseDTO();
                var loginUser = _authService.GetLoginUserInformation();
                if (loginUser.Success)
                {
                    var user = (UserDTO)loginUser.Data;
                    response = await _leaveService.ApplyLeave(request, user.Id);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }
        [Authorize]
        [HttpGet("Leave/Dashboard")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var response = new ResponseDTO();
                var loginUser = _authService.GetLoginUserInformation();
                if (loginUser.Success)
                {
                    var user = (UserDTO)loginUser.Data;
                    response = await _leaveService.Dashboard(user.Id);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }
    }
}

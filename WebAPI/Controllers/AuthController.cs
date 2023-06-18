using Core;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebAPI.Auth.Interface;
using WebAPI.Controllers;

namespace JwtWebApiTutorial.Controllers
{
    public class AuthController : APIControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private ResponseDTO response;
        public AuthController(IAuthService authService, IUserService userService,
                ILogger<AuthController> logger,
                 ILoggerService loggerService
            ) : base(logger, loggerService)
        {
            _authService = authService; 
            _userService = userService;

        }
        [HttpGet("")]
        public ActionResult<string> APIVersion()
        {
            return Ok("V1.0.0");
        }
        [HttpPost("Token")]
        [ProducesResponseType(typeof(UserRequestDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ResponseDTO>> Token(UserRequestDTO request)
        {
            response = new ResponseDTO();
            var validateUser= await _userService.ValidateUser(request);
            if (validateUser.Success)
            {
                var loginUser = (UserDTO)validateUser.Data;
                loginUser.Token =  _authService.CreateToken(loginUser);
                response.Success= true;
                response.Message=validateUser.Message;
                response.Data=loginUser;
                return Ok(response);
            }
            else
            {
                return BadRequest(validateUser);
            }
        }
    }
}

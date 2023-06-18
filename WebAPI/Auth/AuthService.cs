using Core;
using Core.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Auth.Interface;

namespace WebAPI.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private ILoggerService _loggerService;


        public AuthService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILoggerService loggerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration; 
            _loggerService = loggerService;
        }
        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
        public ResponseDTO GetLoginUserInformation()
        {
            var response = new ResponseDTO();
            try
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var user = new UserDTO();
                    user.Id = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid));
                    user.Name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                    user.Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                    user.UserRole = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role));

                    response.Success = true;
                    response.Data=user;
                }
            }
            catch (Exception ex)
            {
                _loggerService.Log(ENUM.ModuleName.Auth.ToString(), ENUM.LoggingLevel.Error.ToString(), ex);
                response.Success = false;
                response.Message = Constants._InternalServerError;
            }
            return response;
        }
        public string CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

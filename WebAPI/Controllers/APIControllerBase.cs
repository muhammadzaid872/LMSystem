using Core;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class APIControllerBase : ControllerBase
    {
        protected ILogger Logger { get; }
        private ILoggerService _loggerService;

        public APIControllerBase(ILogger logger,
                ILoggerService loggerService)
        {
            Logger = logger;
            _loggerService = loggerService;
        }
        protected IActionResult HandleValidationException(ValidationException ex)
        {
            var response = new ResponseDTO();
            response.Message = "Validation failed.";
            response.Success = false;
            response.ErrorList = ex.InnerException!=null?Convert.ToString(ex.InnerException.Message)+"-- stack-trace "+ Convert.ToString(ex.StackTrace) : Convert.ToString(ex.StackTrace);
            _loggerService.Log(ENUM.ModuleName.Validation.ToString(), ENUM.LoggingLevel.Error.ToString(), ex);

            return BadRequest(response);
        }

        protected IActionResult HandleOtherException(Exception ex)
        {
            var response = new ResponseDTO();
            response.Message = "Processing request failed.";
            response.Success = false;
            response.ErrorList = ex.InnerException != null ? Convert.ToString(ex.InnerException.Message) + "-- stack-trace " + Convert.ToString(ex.StackTrace) : Convert.ToString(ex.StackTrace);
            _loggerService.Log(ENUM.ModuleName.Error.ToString(), ENUM.LoggingLevel.Error.ToString(), ex);

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

    }
}

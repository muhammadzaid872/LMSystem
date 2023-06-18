using Core.DTO;
using Core;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Infrastructure.DBModels;

namespace Services
{
    public class UserService : IUserService
    {

        private ILoggerService _loggerService;
        private readonly IDatabaseContext _context;
        ResponseDTO response;
        public UserService(ILoggerService loggerService, IDatabaseContext context)
        {
            _loggerService = loggerService;
            _context = context;
            response = new ResponseDTO();
        }
        public async Task<ResponseDTO> ValidateUser(UserRequestDTO requestDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(requestDTO.Email))
                {
                    response.Success = false;
                    response.Message = Constants._ProvideEmail;
                    return response;
                }
                if (string.IsNullOrEmpty(requestDTO.Password))
                {
                    response.Success = false;
                    response.Message = Constants._ProvidePassword;
                    return response;

                }
                var user = await _context.Users.Where(x => x.Email == requestDTO.Email
                                                        && x.Password == requestDTO.Password
                                                        && x.IsActive == true).
                                                        Select(x => new UserDTO()
                                                        {
                                                            Id = x.Id,
                                                            Name = x.Name,
                                                            Email = x.Email,
                                                            UserRole= x.FkroleId
                                                        }).FirstOrDefaultAsync();
                if (user != null)
                {
                    response.Success = true;
                    response.Message = Constants._UserValidated;
                    response.Data = user;
                }
                else
                {
                    response.Success = false;
                    response.Message = Constants._EmailOrPassworWrong;
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
    }
}

using Core.DTO;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserService
    {
        Task<ResponseDTO> ValidateUser(UserRequestDTO requestDTO);
    }
}

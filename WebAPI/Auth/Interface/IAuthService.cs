using Core;
using Core.DTO;

namespace WebAPI.Auth.Interface
{
    public interface IAuthService
    {
       String GetMyName();
        string CreateToken(UserDTO user);
        ResponseDTO GetLoginUserInformation();
    }
}

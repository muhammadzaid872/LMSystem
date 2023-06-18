using Core;
using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ILeaveService
    {
        Task<ResponseDTO> GetAllLeaves(int userId);
        Task<ResponseDTO> Dashboard(int userId);
        Task<ResponseDTO> ApplyLeave(LeaveRequestDTO leaveRequest,int userId);
    }
}

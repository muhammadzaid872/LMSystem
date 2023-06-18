using Core;
using Core.DTO;
using Infrastructure;
using Infrastructure.DBModels;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LeaveService : ILeaveService
    {
        private ILoggerService _loggerService;
        private readonly IDatabaseContext _context;
        ResponseDTO response;
        public LeaveService(ILoggerService loggerService, IDatabaseContext context)
        {
            _loggerService = loggerService;
            _context = context;
            response = new ResponseDTO();
        }
        public async Task<ResponseDTO> GetAllLeaves(int userId)
        {
            response = new ResponseDTO();
            try
            {
                var leaves = await _context.UserLeaves.OrderByDescending(e => e.Id).Where(x => x.UserId == userId).Select(x => new LeaveDTO()
                {
                    StartDate = x.StartDate.ToString(),
                    EndDate = x.EndDate.ToString(),
                    LeaveType = x.LeaveType,
                    NatureOfLeave = x.NatureOfLeave,
                    Reason = x.Reason,
                    Status = x.Status
                }).ToListAsync();

                response.Success = true;
                response.Message = Constants._RecordGetSuccessfully;
                response.Data= leaves;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ENUM.ModuleName.Leaves.ToString(), ENUM.LoggingLevel.Error.ToString(), ex);
                response.Success = false;
                response.Message = Constants._InternalServerError;
            }
            return response;
        }
        public async Task<ResponseDTO> ApplyLeave(LeaveRequestDTO leaveRequest, int userId)
        {
            response = new ResponseDTO();
            try
            {
                var leave = new UserLeave();

                leave.UserId = userId;
                leave.NatureOfLeave = leaveRequest.NatureOfLeave;
                leave.StartDate = leaveRequest.FromDate;
                leave.EndDate = leaveRequest.ToDate;
                leave.Reason = leaveRequest.Reason;
                leave.LeaveType = leaveRequest.LeaveType;
                leave.NatureOfLeave = leaveRequest.NatureOfLeave;
                leave.Status = "Under-Approval";
                leave.CreationTime= HelperClass._getCurrentDateTime();

                _context.UserLeaves.Add(leave);
               await _context.SaveChangesAsync();
               
                response.Success = true;
                response.Message = Constants._LeaveAppliedSuccessfully;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ENUM.ModuleName.Leaves.ToString(), ENUM.LoggingLevel.Error.ToString(), ex);
                response.Success = false;
                response.Message = Constants._InternalServerError;
            }
            return response;
        }

        public async Task<ResponseDTO> Dashboard(int userId)
        {
            response = new ResponseDTO();
            try
            {
                var leaves = await _context.UserLeaves.Where(x => x.UserId == userId).GroupBy(g=>g.Status)
                    .Select(g => new DashboardCardDTO {
                        Title = g.Key,
                        Value = g.Count().ToString()
                }).ToListAsync();

                response.Success = true;
                response.Message = Constants._RecordGetSuccessfully;
                response.Data = leaves;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ENUM.ModuleName.Leaves.ToString(), ENUM.LoggingLevel.Error.ToString(), ex);
                response.Success = false;
                response.Message = Constants._InternalServerError;
            }
            return response;
        }


    }
}

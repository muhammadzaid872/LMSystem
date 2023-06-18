using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class LeaveRequestDTO
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LeaveType { get; set; }
        public string NatureOfLeave { get; set; }
        public string Reason { get; set; }
    }
}

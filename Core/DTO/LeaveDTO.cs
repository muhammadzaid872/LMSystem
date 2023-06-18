using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class LeaveDTO
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string LeaveType { get; set; }
        public string NatureOfLeave { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}

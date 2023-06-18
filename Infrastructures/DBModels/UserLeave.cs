using System;
using System.Collections.Generic;

namespace Infrastructure.DBModels
{
    public partial class UserLeave
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationTime { get; set; }
        public string? LeaveType { get; set; }
        public string? NatureOfLeave { get; set; }
        public string? Reason { get; set; }
        public int UserId { get; set; }
        public string? Status { get; set; }

        public virtual User User { get; set; } = null!;
    }
}

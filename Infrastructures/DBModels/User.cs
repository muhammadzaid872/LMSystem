using System;
using System.Collections.Generic;

namespace Infrastructure.DBModels
{
    public partial class User
    {
        public User()
        {
            UserLeaves = new HashSet<UserLeave>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreationTime { get; set; }
        public bool? IsActive { get; set; }
        public int? AllowedLeaves { get; set; }
        public int? FkroleId { get; set; }

        public virtual UserRole? Fkrole { get; set; }
        public virtual ICollection<UserLeave> UserLeaves { get; set; }
    }
}

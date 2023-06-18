using Infrastructure.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IDatabaseContext
    {
        DbSet<Logger> Loggers { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserLeave> UserLeaves { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        Task<int> SaveChangesAsync();
    }
}

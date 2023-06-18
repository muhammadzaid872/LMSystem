using System;
using System.Collections.Generic;

namespace Infrastructure.DBModels
{
    public partial class Logger
    {
        public int Id { get; set; }
        public string? ModuleName { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}

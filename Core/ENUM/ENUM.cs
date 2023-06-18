using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core
{
    public static class ENUM
    {
        public enum LoggingLevel
        {
            [Description("Error")]
            Error = 1,
            [Description("Warning")]
            Warning = 2,
            [Description("Info")]
            Info = 3,
            [Description("Request")]
            Request = 4,
        }
        public enum ModuleName
        {
            [Description("Leaves")]
            Leaves = 1,
            [Description("Auth")]
            Auth = 2,
            [Description("Validation")]
            Validation = 3,
            [Description("Error")]
            Error = 4
        }


    }
}

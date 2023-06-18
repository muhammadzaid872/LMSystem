using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ILoggerService
    {
        void Log(string moduleName, string type, Exception exception);
    }
}

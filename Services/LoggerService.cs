using Core;
using Infrastructure;
using Infrastructure.DBModels;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerService: ILoggerService
    {
        private readonly IDatabaseContext _context;

        public LoggerService(IDatabaseContext context)
        {
            _context = context;
        }
        public async void Log(string moduleName, string type, Exception exception)
        {
            var log = new Logger()
            {
                ModuleName = moduleName,
                Type = type,
                Message = exception.StackTrace,
                CreationTime = HelperClass._getCurrentDateTime(),
            };
            _context.Loggers.Add(log);
            await _context.SaveChangesAsync();
        }

    }
}

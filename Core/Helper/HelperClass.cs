using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public  static class HelperClass
    {
        public static DateTime _getCurrentDateTime()
        {
            DateTime date = DateTime.UtcNow.AddHours(4);
            return date;
        }
    }
}

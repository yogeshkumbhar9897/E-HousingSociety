using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLoggerLib
{
    public static class LoggerFactory
    {
        public static ILogger GetLogger(int choice)
        {
            if (choice == 1)
            {
                return FileLogger.GetLogger();
            }
          
            else
            {
                return ConsoleLogger.GetLogger();
            }
        }

    }
}

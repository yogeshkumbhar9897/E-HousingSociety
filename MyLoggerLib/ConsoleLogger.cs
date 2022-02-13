using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLoggerLib
{
    public class ConsoleLogger : ILogger
    {
        private static ConsoleLogger clogger = new ConsoleLogger();
        private ConsoleLogger()
        {
            Debug.WriteLine("Console Logger Object Created");
        }

        public static ConsoleLogger GetLogger()
        {
            return clogger;
        }
        public void Log(string msg)
        {
            Debug.WriteLine("Console Logged: " + msg);
        }
    }
}

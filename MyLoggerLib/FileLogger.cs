using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLoggerLib
{
    public class FileLogger : ILogger
    {

        private static FileLogger flogger = new FileLogger();
        private FileLogger()
        {
            Console.WriteLine("File Logger Object Created");
        }

        public static FileLogger GetLogger()
        {
            return flogger;
        }
        string path = @".\log.txt";
        //private static Logger _logger = new Logger();
        FileStream fs = null;
        StreamWriter writer = null;
        //private Logger() { }
        

        public void Log(string msg)
        {
            try
            {
                if (File.Exists(path))
                {
                    fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                }
                writer = new StreamWriter(fs);

                writer.WriteLine(string.Format("Logged at {0} : {1}", DateTime.Now.ToString(), msg));
                writer.Close();
                fs.Close();
                writer = null;
                fs = null;
            }
            catch (Exception)
            {

                
            }
        }

    }
}

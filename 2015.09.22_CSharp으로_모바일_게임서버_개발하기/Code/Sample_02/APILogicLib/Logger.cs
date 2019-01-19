using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib
{
    public static class Logger
    {
        public static NLog.Logger FileLogger = NLog.LogManager.GetLogger(DateTime.Now.ToString("yyyyMMddHHmm"));


        public static void Debug(string message, int writeType = (int)LOG_TYPE.FILE)
        {
            if (EnableFileWrite(writeType, LOG_TYPE.FILE))
            {
                FileLogger.Debug(message);
            }
        }

        public static void Info(string message, int writeType = (int)LOG_TYPE.FILE)
        {
            if (EnableFileWrite(writeType, LOG_TYPE.FILE))
            {
                FileLogger.Info(message);
            }
        }

        public static void Error(string message, int writeType = (int)LOG_TYPE.FILE)
        {
            if (EnableFileWrite(writeType, LOG_TYPE.FILE))
            {
                FileLogger.Error(message);
            }
        }
        public static void Exception(string message, int writeType = (int)LOG_TYPE.FILE)
        {
            if (EnableFileWrite(writeType, LOG_TYPE.FILE))
            {
                FileLogger.Error(message);
            }
        }


        static bool EnableFileWrite(int write, LOG_TYPE type)
        {
            if ((write & (int)type) == (int)type)
            {
                return true;
            }

            return false;
        }




        public enum LOG_TYPE
        {
            NONE = 0,
            FILE = 1,
            DB  = 2,
            NETWORK = 4,
        }
    }
}

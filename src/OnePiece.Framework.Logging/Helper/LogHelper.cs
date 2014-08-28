using log4net.Config;
using System;
using System.IO;

namespace OnePiece.Framework.Logging
{
    public sealed class LogHelper
    {
        private static log4net.ILog log = null;

        public static log4net.ILog Log
        {
            get
            {
                if (log == null)
                {
                    log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    //FileInfo fi = new FileInfo("Utilities.Log4Net.config");
                    XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utilities.Log4Net.config")));
                }

                return log;
            }
        }

        private static bool initialized = false;

        public static void WindowsServiceInitialize()
        {
            if (!initialized)
            {
                log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utilities.Log4Net.config")));
                initialized = true;
            }
        }

        public static void Debug(string message)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message);
            }
        }

        public static void Info(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        public static void Error(string message)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error(message);
            }
        }
    }
}

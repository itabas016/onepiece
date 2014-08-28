using NLog;
using System;
using System.Text;

namespace OnePiece.Framework.Logging
{
    public class NLogHelper
    {
        public static void Info(string content, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(content);

            LogManager.GetLogger("InfoLogger").Info(content);
        }

        public static void Error(string content)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(content);

            LogManager.GetLogger("ErrorLogger").Error(content);
        }

        public static void Trace(string content)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(content);

            LogManager.GetLogger("TraceLogger").Trace(content);
        }
    }
}

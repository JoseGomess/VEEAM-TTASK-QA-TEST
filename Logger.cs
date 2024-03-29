using System;
using System.IO;

namespace VEEAM_TESTTASK
{
    //This class is responsible for logging entries into the file and command line
    public static class Logger
    {
        private static string logFilePath;

        public static void Initialize(string logFileName, string path)
        {
            string solutionDirectory = AppDomain.CurrentDomain.BaseDirectory;
            logFilePath = Path.Combine(path, logFileName);
        }

        public static void Log(string message)
        {
            LogToConsole(message);
            LogToFile(message);
        }

        private static void LogToConsole(string message)
        {
            Console.WriteLine(message);
        }

        private static void LogToFile(string message)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}

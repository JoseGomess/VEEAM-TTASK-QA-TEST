using System;
using System.IO;
using System.Timers;

namespace VEEAM_TESTTASK
{
    class Program
    {
        private static System.Timers.Timer _timer;
        private static string solutionDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string sourcePath;
        private static string replicaPath;
        private static string logFilePath;

        static void Main(string[] args)
        {
            // Check if there are enough command-line arguments
            if (args.Length < 4)
            {
                // Prompt user for correct usage
                Console.WriteLine("Usage: VEEAM_TESTTASK.exe <source path> <target path> <log path> <interval in seconds>");
                Console.ReadLine(); // Wait for user interaction
                Console.WriteLine("Number of command-line arguments: " + args.Length);
                //return;
            }

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            // Set the source path, target path, log path, and timer interval from the command-line arguments
            sourcePath = Path.Combine(solutionDirectory, args[0]);
            replicaPath = Path.Combine(solutionDirectory, args[1]);
            logFilePath = Path.Combine(solutionDirectory, args[2]);

            Directory.CreateDirectory(sourcePath);
            Directory.CreateDirectory(replicaPath);
            using (File.Create(logFilePath))
            {
                // File.Create() returns a FileStream, but we don't need to do anything with it
                // The using statement ensures that the FileStream is properly disposed
            }

            int interval;
            if (!int.TryParse(args[3], out interval) || interval <= 0)
            {
                Logger.Log("Invalid interval. Interval must be a positive integer.");
                Console.ReadLine();
                return;
            }

            // Initialize logger with log file path
            Logger.Initialize(args[2], solutionDirectory);

            // Start file system watcher for source path
            FSW.StartWatching(sourcePath, ".txt");

            // Start file system watcher for replica path
            FSW.StartWatching(replicaPath, ".txt");

            // Initialize and start timer for folder synchronization
            _timer = new System.Timers.Timer(interval * 1000);
            _timer.Elapsed += SynchronizeFolders;
            _timer.Start();

            // Log synchronization interval
            Logger.Log("The folders will be synchronized every " + args[3] + " seconds.\n");
            Console.ReadLine();
        }

        private static void SynchronizeFolders(object sender, ElapsedEventArgs e)
        {
            Synchronize.SyncFolders(sourcePath, replicaPath);
        }
    }
}

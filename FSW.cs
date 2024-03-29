using System;
using System.IO;

namespace VEEAM_TESTTASK
{
    class FSW
    {
        //altered original source ( https://learn.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?view=net-8.0 )
        public static void StartWatching(string folderPath, string filter)
        {            
            using var watcher = new FileSystemWatcher(folderPath);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = filter;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Watching folder: {folderPath}");
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {                
                return;
            }
            Logger.Log($"Changed: {e.FullPath}");
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Logger.Log(value);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e) =>
            Logger.Log($"Deleted: {e.FullPath}");

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Logger.Log($"Renamed:");
            Logger.Log($"    Old: {e.OldFullPath}");
            Logger.Log($"    New: {e.FullPath}");
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Logger.Log($"Message: {ex.Message}");
                Logger.Log("Stacktrace:");
                Logger.Log($"{ex.StackTrace}");
                
                PrintException(ex.InnerException);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEEAM_TESTTASK
{
    public class Synchronize
    {
        public static void SyncFolders(string sourcePath, string replicaPath)
        {
            try
            {
                Logger.Log("Starting synchronization...");
                SyncDirectory(sourcePath, replicaPath);
                Logger.Log("Synchronization complete.");
            }
            catch (Exception ex)
            {
                Logger.Log($"Error during synchronization: {ex.Message}");
            }
        }

        private static void SyncDirectory(string sourceDir, string targetDir)
        {
            // Create target directory if it doesn't exist
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            // Copy files
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string targetFile = Path.Combine(targetDir, Path.GetFileName(file));
                File.Copy(file, targetFile, true); // Overwrite existing files
                Logger.Log($"Copied file: {file} -> {targetFile}");
            }

            // Recursively synchronize subdirectories
            foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
            {
                string targetSubDir = Path.Combine(targetDir, Path.GetFileName(sourceSubDir));
                SyncDirectory(sourceSubDir, targetSubDir);
            }

            // Remove files and directories from target that don't exist in source
            foreach (string targetFile in Directory.GetFiles(targetDir))
            {
                string sourceFile = Path.Combine(sourceDir, Path.GetFileName(targetFile));
                if (!File.Exists(sourceFile))
                {
                    File.Delete(targetFile);
                    Logger.Log($"Deleted file: {targetFile}");
                }
            }

            foreach (string targetSubDir in Directory.GetDirectories(targetDir))
            {
                string sourceSubDir = Path.Combine(sourceDir, Path.GetFileName(targetSubDir));
                if (!Directory.Exists(sourceSubDir))
                {
                    Directory.Delete(targetSubDir, true); // Recursive delete
                    Logger.Log($"Deleted directory: {targetSubDir}");
                }
            }
        }
    }
}

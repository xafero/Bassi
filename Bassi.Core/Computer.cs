using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Environment;

namespace Bassi.Core
{
    public class Computer : IDisposable
    {
        private static ILog log = LogManager.GetLogger(typeof(Computer));

        public IDictionary<SpecialFolder, DirectoryInfo> SpecialFolders { get; }
        public IDictionary<string, DirectoryInfo> Drives { get; }

        public Computer()
        {
            SpecialFolders = FindSpecialFolders().OrderBy(e => e.Key + "").ToSimpleDict();
            Drives = FindDrives().OrderBy(e => e.Key).ToSimpleDict();
        }

        private static IEnumerable<SpecialFolder> AllSpecialFolders => Enum.GetValues(typeof(SpecialFolder)).OfType<SpecialFolder>();
        private static IEnumerable<DriveInfo> AllReadyDrives => DriveInfo.GetDrives().Where(d => d.IsReady);

        private static IEnumerable<KeyValuePair<string, DirectoryInfo>> FindDrives()
        {
            foreach (var drive in AllReadyDrives)
            {
                log.DebugFormat("Found drive '{0}' {1} {2} {3} {4} {5} {6} {7}",
                    drive.Name, drive.AvailableFreeSpace, drive.DriveFormat,
                    drive.DriveType, drive.RootDirectory, drive.TotalFreeSpace,
                    drive.TotalSize, drive.VolumeLabel);
                var name = drive.VolumeLabel;
                var info = drive.RootDirectory;
                if (string.IsNullOrWhiteSpace(name) || !info.Exists)
                    continue;
                yield return new KeyValuePair<string, DirectoryInfo>(name, info);
            }
        }

        private static IEnumerable<KeyValuePair<SpecialFolder, DirectoryInfo>> FindSpecialFolders()
        {
            foreach (var name in AllSpecialFolders)
            {
                var folder = GetFolderPath(name);
                DirectoryInfo info;
                if (string.IsNullOrWhiteSpace(folder) || !(info = new DirectoryInfo(folder)).Exists)
                    continue;
                log.DebugFormat("Found special '{0}' {1}", name, folder);
                yield return new KeyValuePair<SpecialFolder, DirectoryInfo>(name, info);
            }
        }

        public void Dispose()
        {
            Drives.Clear();
            SpecialFolders.Clear();
        }
    }
}
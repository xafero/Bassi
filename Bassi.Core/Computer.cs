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
        public static IDictionary<string, DateTime> HumanDates { get; private set; }

        static Computer()
        {
            var now = DateTime.UtcNow;
            HumanDates = Enumerable.Range(0, 60).Select(i => now.AddSeconds(-i).ToHumanDate())
                        .Concat(Enumerable.Range(0, 60).Select(i => now.AddMinutes(-i).ToHumanDate()))
                        .Concat(Enumerable.Range(0, 25).Select(i => now.AddHours(-i).ToHumanDate()))
                        .Concat(Enumerable.Range(0, 32).Select(i => now.AddDays(-i).ToHumanDate()))
                        .Concat(Enumerable.Range(0, 13).Select(i => now.AddMonths(-i).ToHumanDate()))
                        .Concat(Enumerable.Range(0, 20).Select(i => now.AddYears(-i).ToHumanDate()))
                        .ToSimpleDict();
        }

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
                    drive.Name, LinqExts.Try(() => drive.AvailableFreeSpace), drive.DriveFormat,
                    drive.DriveType, drive.RootDirectory, LinqExts.Try(() => drive.TotalFreeSpace),
                    LinqExts.Try(() => drive.TotalSize), drive.VolumeLabel);
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
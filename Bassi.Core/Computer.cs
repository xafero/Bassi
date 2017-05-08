using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bassi.Core
{
    public class Computer
    {
        private static ILog log = LogManager.GetLogger(typeof(Computer));

        public void FindDrives()
        {
            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                log.InfoFormat("Found drive '{0}' {1} {2} {3} {4} {5} {6} {7}",
                    drive.Name,
                    drive.AvailableFreeSpace,
                    drive.DriveFormat,
                    drive.DriveType,
                    drive.RootDirectory,
                    drive.TotalFreeSpace,
                    drive.TotalSize,
                    drive.VolumeLabel);

            }
        }
    }
}
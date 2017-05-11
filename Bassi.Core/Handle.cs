using System;
using System.Diagnostics;
using System.IO;

namespace Bassi.Core
{
    public class Handle : IHandle
    {
        private string FilePath { get; }
        private Lazy<FileInfo> Info { get; }
        private Lazy<IFilterRef> Refs { get; }
        private Lazy<FileVersionInfo> Version { get; }

        public Handle(string path, Func<string, IFilter> func)
        {
            FilePath = Path.GetFullPath(path);
            Ext = Path.GetExtension(FilePath).ToLowerInvariant().TrimStart('.');
            Name = Path.GetFileNameWithoutExtension(FilePath);
            Dir = Path.GetDirectoryName(FilePath);
            Info = new Lazy<FileInfo>(() => new FileInfo(FilePath));
            Refs = new Lazy<IFilterRef>(() => new LambdaRefer(FilePath, func));
            Version = new Lazy<FileVersionInfo>(() => FileVersionInfo.GetVersionInfo(FilePath));
        }

        public string Dir { get; }
        public string Name { get; }
        public string Ext { get; }
        public long Size => Info.Value.Length;
        public long Created => Info.Value.CreationTimeUtc.Ticks;
        public long Accessed => Info.Value.LastAccessTimeUtc.Ticks;
        public long Changed => Info.Value.LastWriteTimeUtc.Ticks;
        public IFilterRef R => Refs.Value;
        public FileVersionInfo V => Version.Value;
    }
}
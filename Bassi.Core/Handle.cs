﻿using ByteSizeLib;
using System;
using System.IO;

namespace Bassi.Core
{
    public class Handle : IHandle
    {
        private string FilePath { get; }
        private Lazy<FileInfo> Info { get; }

        public Handle(string path)
        {
            FilePath = Path.GetFullPath(path);
            Ext = Path.GetExtension(FilePath).ToLowerInvariant().TrimStart('.');
            Name = Path.GetFileNameWithoutExtension(FilePath);
            Dir = Path.GetDirectoryName(FilePath);
            Info = new Lazy<FileInfo>(() => new FileInfo(FilePath));
        }

        public string Dir { get; }
        public string Name { get; }
        public string Ext { get; }
        public long Size => Info.Value.Length;
    }

    public interface IHandle
    {
        string Dir { get; }
        string Name { get; }
        string Ext { get; }
        long Size { get; }
    }
}
using System;
using System.IO;
using System.Security.Cryptography;

namespace Bassi.Core
{
    public static class FileUtils
    {
        public static string CreateHash(string file)
        {
            using (var fs = File.OpenRead(file))
            {
                var sha = new SHA1Managed();
                var hex = BitConverter.ToString(sha.ComputeHash(fs));
                return hex.Replace("-", "").ToLowerInvariant();
            }
        }

        public static void CreateDirIfNeeded(string root)
        {
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
        }

        public static void Copy(string source, string target)
        {
            using (var input = File.OpenRead(source))
            using (var output = File.Create(target))
                input.CopyTo(output);
        }
    }
}
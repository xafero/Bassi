using Bassi.Core;
using log4net.Config;
using System;
using System.IO;
using System.Linq;

namespace Bassi.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            using (var computer = new Computer())
            {
                var folders = computer.SpecialFolders;
                var filters = new IFilter[] { /*new PictureFilter(),*/ new VideoFilter(), new DocumentFilter() };
                foreach (var path in folders.Select(f => f.Value.FullName).Distinct())
                {
                    try
                    {
                        foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
                        {
                            var matched = filters.SingleOrDefault(f => f.IsValid(file));
                            if (matched == null)
                                continue;
                            Console.WriteLine(matched.GetType().Name + " => " + file);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.Error.WriteLine("No access to '{0}'!", path);
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
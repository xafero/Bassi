using Bassi.Core;
using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using static Bassi.Core.Config;

namespace Bassi.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            var appCfg = ConfigurationManager.AppSettings;
            var configFile = appCfg["config"];
            var jsonCfg = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = { new StringEnumConverter() }
            };
            var json = File.ReadAllText(configFile);
            var cfg = JsonConvert.DeserializeObject<Config>(json, jsonCfg);
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

                // Console.ReadLine();
            }
        }
    }
}
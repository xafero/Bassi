using Bassi.Core;
using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
            var userFilters = cfg.Filters.ToDictionary(k => k.Name, v => v.ToLambda());
            using (var computer = new Computer())
            {
                filters = userFilters.ToFilters(GetFilter).ToArray();
                var userRules = cfg.Rules.ToDictionary(k => Path.Combine(Path.GetFullPath(k.Target), k.Name),
                    v => GetFilter(v.Filter));
                var folders = computer.SpecialFolders;
                foreach (var path in folders.Select(f => f.Value.FullName).Distinct())
                {
                    try
                    {
                        foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
                        {
                            var matched = userRules.SingleOrDefault(f => f.Value.IsValid(file));
                            if (matched.Key == null || matched.Value == null)
                                continue;
                            var destDir = matched.Key;
                            Console.WriteLine($" * [{matched.Value.Name}] {file} => {destDir}");
                            if (!Directory.Exists(destDir))
                                Directory.CreateDirectory(destDir);
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

        private static IFilter[] filters;

        private static IFilter GetFilter(string name)
            => filters.First(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}
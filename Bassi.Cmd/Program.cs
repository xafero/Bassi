using Bassi.Core;
using log4net.Config;
using Newtonsoft.Json;
using System;

namespace Bassi.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            Console.WriteLine(JsonConvert.SerializeObject(new Computer(), Formatting.Indented));
            Console.ReadLine();
        }
    }
}
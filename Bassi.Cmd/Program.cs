using Bassi.Core;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bassi.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            var pc = new Computer();
            pc.FindDrives();
            pc.FindSpecialFolders();
            Console.ReadLine();
        }
    }
}
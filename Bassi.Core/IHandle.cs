using System.Collections.Generic;
using System.Diagnostics;

namespace Bassi.Core
{
    public interface IHandle
    {
        string Dir { get; }
        string Name { get; }
        string Ext { get; }
        long Size { get; }
        long Created { get; }
        long Accessed { get; }
        long Changed { get; }
        IFilterRef R { get; }
        FileVersionInfo V { get; }
        IDictionary<string, string> I { get; }
    }
}
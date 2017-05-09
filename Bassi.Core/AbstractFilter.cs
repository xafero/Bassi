using System.IO;

namespace Bassi.Core
{
    public abstract class AbstractFilter : IFilter
    {
        public abstract string Name { get; }

        protected abstract bool IsValid(Handle file);

        public bool IsValid(string file)
        {
            var ext = Path.GetExtension(file).ToLowerInvariant().TrimStart('.');
            return IsValid(new Handle { Ext = ext });
        }
    }
}
using System.IO;

namespace Bassi.Core
{
    public abstract class AbstractFilter : IFilter
    {
        protected abstract bool IsValidExtension(string ext);

        public bool IsValid(string file)
        {
            var ext = Path.GetExtension(file).ToLowerInvariant().TrimStart('.');
            return IsValidExtension(ext);
        }
    }
}
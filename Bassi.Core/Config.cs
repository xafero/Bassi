using System.Collections.Generic;

namespace Bassi.Core
{
    public class Config
    {
        public IList<Filter> Filters { get; set; } = new List<Filter>();

        public class Filter
        {
            public string Name { get; set; }
            public string Query { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bassi.Core
{
    public static class LinqExts
    {
        public static IDictionary<T, V> ToSimpleDict<T, V>(this IEnumerable<KeyValuePair<T, V>> items)
        {
            var dict = new Dictionary<T, V>();
            foreach (var item in items)
                dict[item.Key] = item.Value;
            return dict;
        }

        public static IEnumerable<IFilter> ToFilters(this IEnumerable<KeyValuePair<string, Func<Handle, bool>>> pairs)
            => pairs.Select(p => new LambdaFilter(p.Key, p.Value));
    }
}
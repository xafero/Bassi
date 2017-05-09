using System.Collections.Generic;

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
    }
}
using ByteSizeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static IEnumerable<IFilter> ToFilters(this IEnumerable<KeyValuePair<string, Func<IHandle, bool>>> pairs)
            => pairs.Select(p => new LambdaFilter(p.Key, p.Value));

        public static string Transform(string query)
        {
            var builder = new StringBuilder();
            var parts = query.Split(' ');
            foreach (var part in parts)
            {
                builder.Append(" ");
                if (ByteSize.TryParse(part, out ByteSize bytes))
                {
                    builder.Append(bytes.Bytes);
                    continue;
                }
                builder.Append(part);
            }
            return builder.ToString().Trim();
        }
    }
}
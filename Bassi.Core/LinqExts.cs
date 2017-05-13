using ByteSizeLib;
using Humanizer;
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

        public static IEnumerable<IFilter> ToFilters(this IEnumerable<KeyValuePair<string, Func<IHandle, bool>>> pairs,
            Func<string, IFilter> refer) => pairs.Select(p => new LambdaFilter(p.Key, p.Value, refer));

        public static KeyValuePair<string, DateTime> ToHumanDate(this DateTime dateTime)
            => new KeyValuePair<string, DateTime>(dateTime.Humanize(), dateTime);

        public static string Transform(string query)
        {
            foreach (var humanDate in Computer.HumanDates)
                if (query.Contains(humanDate.Key))
                    query = query.Replace(humanDate.Key, humanDate.Value.Ticks + string.Empty);
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

        public static T Try<T>(Func<T> getter)
        {
            try
            {
                return getter();
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
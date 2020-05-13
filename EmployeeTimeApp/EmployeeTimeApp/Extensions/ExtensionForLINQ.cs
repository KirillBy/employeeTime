using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeApp.Class
{
    //Extension for LINQ which return
    public static  class ExtensionForLINQ
    {
        public static IEnumerable<TSource> TopWithTies<TSource, TValue>
           (this IEnumerable<TSource> source,
            int count,
            Func<TSource, TValue> selector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (count == 0) yield break;
            using (var iter = source.OrderByDescending(selector).GetEnumerator())
            {
                if (iter.MoveNext())
                {
                    yield return iter.Current;
                    while (--count >= 0)
                    {
                        if (!iter.MoveNext()) yield break;
                        yield return iter.Current;
                    }
                    var lastVal = selector(iter.Current);
                    var eq = EqualityComparer<TValue>.Default;
                    while (iter.MoveNext() && eq.Equals(lastVal, selector(iter.Current)))
                    {
                        yield return iter.Current;
                    }
                }
            }
        }
    }
}

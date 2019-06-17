using System;
using System.Collections.Generic;
using System.Linq;

namespace WsdPreprocessingStudio.Core.Extensions
{
    public static class LinqExtensions
    {
        public static TValue MinOrDefault<TValue>(
            this IEnumerable<TValue> items)
        {
            var enumerable = items as TValue[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Min() : default(TValue);
        }

        public static TValue MinOrDefault<TItem, TValue>(
            this IEnumerable<TItem> items, Func<TItem, TValue> selector)
        {
            var enumerable = items as TItem[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Min(selector) : default(TValue);
        }

        public static double AverageOrDefault(
            this IEnumerable<int> items)
        {
            var enumerable = items as int[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Average() : 0d;
        }

        public static double AverageOrDefault(
            this IEnumerable<long> items)
        {
            var enumerable = items as long[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Average() : 0d;
        }

        public static double AverageOrDefault(
            this IEnumerable<double> items)
        {
            var enumerable = items as double[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Average() : 0d;
        }

        public static double AverageOrDefault<TItem>(
            this IEnumerable<TItem> items, Func<TItem, double> selector)
        {
            var enumerable = items as TItem[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Average(selector) : 0d;
        }

        public static TValue MaxOrDefault<TValue>(
            this IEnumerable<TValue> items)
        {
            var enumerable = items as TValue[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Max() : default(TValue);
        }

        public static TValue MaxOrDefault<TItem, TValue>(
            this IEnumerable<TItem> items, Func<TItem, TValue> selector)
        {
            var enumerable = items as TItem[] ?? items.ToArray();

            return enumerable.Any() ? enumerable.Max(selector) : default(TValue);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace EnumerableCombineExtensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> CombineWith<T> (this IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (var item in first)
            {
                yield return item;
            }
            foreach (var item in second)
            {
                yield return item;
            }
        }
        public static IEnumerable<T> Combine<T>(this (IEnumerable<T> first, IEnumerable<T> second) enumerables)
        {
            return enumerables.first.CombineWith(enumerables.second);
        }
        public static IEnumerable<T> Combine<T>(this (IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third) enumerables)
        {
            return enumerables.first.CombineWith(enumerables.second).CombineWith(enumerables.third);
        }
    }
}

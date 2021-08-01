using System;
using System.Collections.Generic;

namespace EnumerableCombineExtensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Combine<T>(this (IEnumerable<T> first, IEnumerable<T> second) enumerables)
        {
            foreach (var item in enumerables.first)
            {
                yield return item;
            }
            foreach (var item in enumerables.second)
            {
                yield return item;
            }
        }
        public static IEnumerable<T> Combine<T>(this (IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third) enumerables)
        {
            foreach (var item in enumerables.first)
            {
                yield return item;
            }
            foreach (var item in enumerables.second)
            {
                yield return item;
            }
            foreach (var item in enumerables.third)
            {
                yield return item;
            }
        }
    }
}

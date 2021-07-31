using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal static class IEnumerableExtensions
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

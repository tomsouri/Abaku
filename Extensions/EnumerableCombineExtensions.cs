using System;
using System.Collections.Generic;

namespace EnumerableCombineExtensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Attaches two enumerables of the same type together.
        /// </summary>
        /// <typeparam name="T">The type of items in the given enumerables.</typeparam>
        /// <param name="first">The enumerable, that should be enumerated first.</param>
        /// <param name="second">The second enumerable.</param>
        /// <returns>One enumerable, in which the second enumerable is added to the end of the first one.</returns>
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

        /// <summary>
        /// Connects two enumerables of the same type together.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerables"></param>
        /// <returns>Connected enumerable.</returns>
        public static IEnumerable<T> Combine<T>(this (IEnumerable<T> first, IEnumerable<T> second) enumerables)
        {
            return enumerables.first.CombineWith(enumerables.second);
        }

        /// <summary>
        /// Connects three enumerables of the same type together.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerables"></param>
        /// <returns>Connected enumerable.</returns>
        public static IEnumerable<T> Combine<T>(this (IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third) enumerables)
        {
            return enumerables.first.CombineWith(enumerables.second).CombineWith(enumerables.third);
        }
    }
}

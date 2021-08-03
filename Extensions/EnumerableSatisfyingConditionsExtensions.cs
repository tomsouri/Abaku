using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerableSatisfyingConditionsExtensions
{
    public static class EnumerableSatisfyingConditionsExtensions
    {
        /// <summary>
        /// Does at least one element of the IEnumerable<Position> satisfy the condition given by predicate?
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns>True if at least one element satisfies the condition.</returns>
        public static bool AtLeastOneSatisfies<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            foreach (var item in enumerable)
            {
                if (predicate(item)) return true;
            }
            return false;
        }

        /// <summary>
        /// Are all items in the enumerable equal, compared using delegate equalityComparison?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="equalityComparison"></param>
        /// <returns>True if all Positions are equal.</returns>
        public static bool AllEqual<T>(this IEnumerable<T> enumerable, Func<T, T, bool> equalityComparison)
        {
            if (!enumerable.Any()) return true;
            T first = enumerable.First();
            return enumerable.Skip(1).All(x => equalityComparison(first, x));
        }

        public static bool ArePairwiseDistinct<T>(this IEnumerable<T> enumerable) where T : IEquatable<T>
        {
            int itemsCount = 0;
            int equalitiesCount = 0;
            foreach (var item1 in enumerable)
            {
                itemsCount++;
                foreach (var item2 in enumerable)
                {
                    if (item1.Equals(item2)) equalitiesCount++;
                }
            }
            // Every position is equal to itself.
            // If the positions are distinct, the number of equalities
            // is the same as the number of positions.
            return itemsCount == equalitiesCount;
        }


        /// <summary>
        /// Finds first not-null item in enumerable.
        /// If there is no such item, returns null.
        /// </summary>
        /// <typeparam name="T">Type of items.</typeparam>
        /// <param name="enumerable">Enumerable to look for items.</param>
        /// <returns>Not-null item or null.</returns>
        public static T FindFirstNotNull<T>(this IEnumerable<T> enumerable) where T : class
        {
            foreach (var item in enumerable)
            {
                if (item != null) return item;
            }
            return null;
        }
    }
}

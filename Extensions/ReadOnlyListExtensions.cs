using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace ReadOnlyListExtensions
{
    public static class IReadOnlyListExtensions
    {
        /// <summary>
        /// Get all sections of given list, 
        /// such that the given index is contained in the section.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="list">The list to get sections of.</param>
        /// <param name="containedIndex">Index that should be contained in all the sections.</param>
        /// <returns>Enumerable of section bounds (start and end indices as value tuple).</returns>
        public static IEnumerable<IReadOnlyList<T>> GetSectionsContainingIndex<T>(this IReadOnlyList<T> list,
                                                                                  int containedIndex)
        {
            foreach (var (startIndex, endIndex) in GetSectionsContainingIndexBounds(list,containedIndex))
            {
                yield return new ReadOnlyListSegment<T>(list, startIndex, endIndex - startIndex + 1);
            }
        }

        /// <summary>
        /// Get all sections of given list, 
        /// such that the given indices are contained in the section.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="list">The list to get sections of.</param>
        /// <param name="containedIndex1">Index that should be contained in all the sections.</param>
        /// <param name="containedIndex2">Other index that should be contained in all the sections.</param>
        /// <returns>Enumerable of section bounds (start and end indices as value tuple).</returns>
        public static IEnumerable<IReadOnlyList<T>> GetSectionsContainingIndices<T>(this IReadOnlyList<T> list,
                                                                          int containedIndex1,
                                                                          int containedIndex2)
        {
            foreach (var (startIndex, endIndex) in GetSectionsContainingIndicesBounds(list, containedIndex1, containedIndex2))
            {
                yield return new ReadOnlyListSegment<T>(list, startIndex, endIndex - startIndex + 1);
            }
        }


        /// <summary>
        /// Get bounds of all sections of given list, 
        /// such that the given index is contained in the section.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="list">The list to get sections of.</param>
        /// <param name="containedIndex">Index that should be contained in all the sections.</param>
        /// <returns>Enumerable of section bounds (start and end indices as value tuple).</returns>
        public static IEnumerable<(int startIndex, int endIndex)> GetSectionsContainingIndexBounds<T>(
            this IReadOnlyList<T> list, int containedIndex)
        {
            int minimum = 0;
            int maximum = list.Count - 1;
            return GetBounds(minimum, containedIndex, maximum);
        }

        /// <summary>
        /// Get bounds of all sections of given list, 
        /// such that the given index is contained in the section
        /// and the other index is not.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="list">List to find sections of.</param>
        /// <param name="containedIndex">Index contained in the sections.</param>
        /// <param name="missingIndex">Index NOT contained in the sections.</param>
        /// <returns>Enumerable of section bounds (start and end indices as value tuple).</returns>
        public static IEnumerable<(int startIndex, int endIndex)> GetSectionsContainingIndexNotOtherBounds<T>(
            this IReadOnlyList<T> list, int containedIndex, int missingIndex)
        {
            if (containedIndex == missingIndex)
            {
                throw new InvalidOperationException(
                    "Arguments " + nameof(containedIndex) + " and " + nameof(missingIndex) + " must NOT be equal."
                    );
            }

            int minimum = (containedIndex < missingIndex) ? 0 : missingIndex + 1;
            int maximum = (containedIndex < missingIndex) ? missingIndex - 1 : list.Count - 1;

            return GetBounds(minimum, containedIndex, maximum);
        }

        /// <summary>
        /// Get bounds of all sections of given list, 
        /// such that the given indices are contained in the section.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="list">The list to get sections of.</param>
        /// <param name="containedIndex1">Index that should be contained in all the sections.</param>
        /// <param name="containedIndex2">Other index that should be contained in all the sections.</param>
        /// <returns>Enumerable of section bounds (start and end indices as value tuple).</returns>
        public static IEnumerable<(int startIndex, int endIndex)> GetSectionsContainingIndicesBounds<T>(
            this IReadOnlyList<T> list, int containedIndex1, int containedIndex2)
        {
            int minimum = 0;
            int maximum = list.Count - 1;

            return GetBounds(minimum, containedIndex1, containedIndex2, maximum);
        }


        /// <summary>
        /// Get bounds of all sections, startIndex at least minimum,
        /// endIndex is at most maximum
        /// and the given index is contained in the section.
        /// </summary>
        /// <param name="containedIndex">Index contained in the sections.</param>
        /// <returns>Enumerable of section bounds (start and end indices as value tuple).</returns>
        private static IEnumerable<(int startIndex, int endIndex)> GetBounds(int minimum,
                                                                             int containedIndex,
                                                                             int maximum)
        {
            return GetBounds(minimum, containedIndex, containedIndex, maximum);
        }

        /// <summary>
        /// Get bounds of all sections, startIndex at least minimum,
        /// endIndex is at most maximum
        /// and the given indices are contained in the section.
        /// </summary>
        /// <param name="containedIndex1">Index contained in the sections.</param>
        /// <param name="containedIndex2">Other index contained in the sections.</param>
        /// <returns>Enumerable of section bounds (start and end indices as value tuple).</returns>
        private static IEnumerable<(int startIndex, int endIndex)> GetBounds(int minimum,
                                                                             int containedIndex1,
                                                                             int containedIndex2,
                                                                             int maximum)
        {
            if (containedIndex1 > containedIndex2)
            {
                (containedIndex2, containedIndex1) = (containedIndex1, containedIndex2);
            }

            for (int startIndex = minimum; startIndex <= containedIndex1; startIndex++)
            {
                for (int endIndex = containedIndex2; endIndex <= maximum; endIndex++)
                {
                    yield return (startIndex, endIndex);
                }
            }
        }
    }
}

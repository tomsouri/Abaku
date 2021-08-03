using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using ReadOnlyListExtensions.Bounds;

namespace ReadOnlyListExtensions
{
    namespace Sections
    {
        public static class GetSectionsReadOnlyListExtensions
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
                foreach (var (startIndex, endIndex) in list.GetSectionsContainingIndexBounds(containedIndex))
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
                foreach (var (startIndex, endIndex) in list.GetSectionsContainingIndicesBounds(containedIndex1, containedIndex2))
                {
                    yield return new ReadOnlyListSegment<T>(list, startIndex, endIndex - startIndex + 1);
                }
            }
        }
    }
}

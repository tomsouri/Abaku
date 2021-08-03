using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace ReadOnlyListExtensions
{
    namespace Splitting
    {
        public static class SplitReadOnlyListExtensions
        {
            public static IEnumerable<(IReadOnlyList<T>, IReadOnlyList<T>)> SplitIntoTwoParts<T>(this IReadOnlyList<T> list)
            {
                for (int firstCount = 1; firstCount < list.Count; firstCount++)
                {
                    yield return (new ReadOnlyListSegment<T>(list, startIndex:0, firstCount), 
                        new ReadOnlyListSegment<T>(list, startIndex:firstCount, count: list.Count - firstCount));
                }
            }
            public static IEnumerable<(IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>)> SplitIntoThreeParts<T>(this IReadOnlyList<T> list)
            {
                for (int  firstCount = 1;  firstCount < list.Count - 1;  firstCount++)
                {
                    for (int secondCount = 1; firstCount + secondCount < list.Count; secondCount++)
                    {
                        var thirdCount = list.Count - firstCount - secondCount;
                        yield return (new ReadOnlyListSegment<T>(list, startIndex: 0, firstCount),
                            new ReadOnlyListSegment<T>(list, startIndex: firstCount, secondCount),
                            new ReadOnlyListSegment<T>(list, startIndex: firstCount + secondCount, thirdCount));
                    }
                }
            }

            /// <summary>
            /// Works only for partsCount = 1, 2 or 3.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="list"></param>
            /// <param name="partsCount"></param>
            /// <returns></returns>
            public static IEnumerable<IReadOnlyList<T>[]> SplitIntoParts<T>(this IReadOnlyList<T> list, int partsCount)
            {
                if (partsCount == 1) yield return new IReadOnlyList<T>[] { list };
                else if (partsCount == 2)
                {
                    foreach (var (p1,p2) in SplitIntoTwoParts<T>(list))
                    {
                        yield return new IReadOnlyList<T>[] { p1, p2 };
                    }
                }
                else if (partsCount == 3)
                {
                    foreach (var (p1, p2, p3) in SplitIntoThreeParts<T>(list))
                    {
                        yield return new IReadOnlyList<T>[] { p1, p2, p3 };
                    }
                }
                else
                {
                    // TODO: implement, when a more than binary operators are needed.
                    throw new NotImplementedException();
                }
            }
        }
    }
}


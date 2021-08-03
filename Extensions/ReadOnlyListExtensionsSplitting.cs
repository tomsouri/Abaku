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
            public static IEnumerable<(IReadOnlyList<T>, IReadOnlyList<T>)> SplitIntoTwoParts<T>(IReadOnlyList<T> list)
            {
                for (int firstCount = 1; firstCount < list.Count; firstCount++)
                {
                    yield return   (new ReadOnlyListSegment<T>(list, startIndex:0, firstCount), 
                        new ReadOnlyListSegment<T>(list, startIndex:firstCount, count: list.Count - firstCount));
                }
            }
            public static IEnumerable<(IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>)> SplitToThreeParts<T>(IReadOnlyList<T> list)
            {
                throw new NotImplementedException();
            }
            public static IEnumerable<IReadOnlyList<T>[]> SplitToParts<T>(IReadOnlyList<T> list, int partsCount)
            {
                throw new NotImplementedException();
            }
        }
    }
}


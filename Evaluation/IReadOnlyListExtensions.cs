using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation
{
    internal static class IReadOnlyListExtensions
    {
        public static IEnumerable<IReadOnlyList<T>> GetSectionsContainingIndex<T>(this IReadOnlyList<T> list,
                                                                                  int containedIndex)
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<IReadOnlyList<T>> GetSectionsWithOneIndexWithoutOther<T>(this IReadOnlyList<T> list,
                                                                                           int containedIndex,
                                                                                           int missingIndex)
        {
            throw new NotImplementedException();
        }
    }
}
